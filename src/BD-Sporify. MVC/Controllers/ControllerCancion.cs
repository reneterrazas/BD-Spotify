using Microsoft.AspNetCore.Mvc;
using BD_Sporify._MVC.Models;
using Spotify.Core;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;



namespace SpotifyMVC.Controllers
{
    public class CancionController : Controller
    {
        private readonly ILogger<CancionController> _logger;
        private readonly IRepoCancionAsync repoCancion;
        private readonly IRepoGeneroAsync repoGenero;
        private readonly IRepoArtistaAsync repoArtista;
        private readonly IRepoAlbumAsync repoAlbum;

        public CancionController(
            ILogger<CancionController> logger,
            IRepoCancionAsync repoCancion,
            IRepoArtistaAsync repoArtista,
            IRepoAlbumAsync repoAlbum,
            IRepoGeneroAsync repoGenero)
        {

            _logger = logger;
            this.repoCancion = repoCancion;
            this.repoArtista = repoArtista;
            this.repoAlbum = repoAlbum;
            this.repoGenero = repoGenero;
        }

        // GET: mostrar formulario
        public async Task<IActionResult> Index()
        {

            var canciones = await repoCancion.Obtener();
            return View(canciones);
        }
        [HttpGet]
        public async Task<IActionResult> CreateCancion(){
            
            var vm = new CrearCancionViewModel{
                albums = await repoAlbum.Obtener(),
                generos = await repoGenero.Obtener()
            };

            return View(vm);
        }

        // POST: dar de alta Cancion
        [HttpPost]
        public async Task<IActionResult> CreateCancion(CrearCancionViewModel model)
        {
            if (ModelState.IsValid)
            {
                var albumSeleccionado = await repoAlbum.DetalleDe(model.AlbumId);
                var generoSeleccionado = await repoGenero.DetalleDe(model.GeneroId);

                var cancion = new Cancion
                {
                    Titulo = model.Titulo,
                    duration = new TimeSpan(0, 4, 23),
                    album = albumSeleccionado,
                    artista = albumSeleccionado.artista,
                    genero = generoSeleccionado
                };

                await repoCancion.Alta(cancion);
                return RedirectToAction("Index");
            }

            model.generos = await repoGenero.Obtener();
            model.albums = await repoAlbum.Obtener();

            return View(model);
        }

    
    [HttpGet]
    public async Task<IActionResult> DetalleCancion(uint id){
        
        var cancion = await repoCancion.DetalleDe(id);

        return View(cancion);
    }
    }
}