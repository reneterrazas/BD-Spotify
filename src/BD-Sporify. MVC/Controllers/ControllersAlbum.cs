using Microsoft.AspNetCore.Mvc;
using BD_Sporify._MVC.Models;
using Spotify.Core;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;



namespace SpotifyMVC.Controllers
{
    public class AlbumController : Controller
    {
        private readonly ILogger<AlbumController> _logger;
        private readonly IRepoArtistaAsync repoArtista;
        private readonly IRepoAlbumAsync repoAlbum;

        public AlbumController(
            ILogger<AlbumController> logger,
            IRepoArtistaAsync repoArtista,
            IRepoAlbumAsync repoAlbum)
        {
            _logger = logger;
            this.repoArtista = repoArtista;
            this.repoAlbum = repoAlbum;
        }

        // GET: mostrar formulario
        public async Task<IActionResult> Index()
        {

            var albums = await repoAlbum.Obtener();

            return View(albums);
        }
        [HttpGet]
        public async Task<IActionResult> CreateAlbum(){
            
            var vm = new AlbumViewModel{
                artistas = await repoArtista.Obtener()
            };

            return View(vm);
        }

        // POST: dar de alta álbum
        [HttpPost]
        public async Task<IActionResult> CreateAlbum(AlbumViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Traer el artista completo por su ID
                var artistaSeleccionado = await repoArtista.DetalleDe(model.ArtistaId);

                // Crear el Album usando el objeto artista
                var album = new Album
                {
                    Titulo = model.Titulo,
                    FechaLanzamiento = model.FechaLanzamiento,
                    artista = artistaSeleccionado
                };

                await repoAlbum.Alta(album);
                return RedirectToAction("Index");
            }

            model.artistas = await repoArtista.Obtener();
            return View(model);
        }

    
    [HttpGet]
    public async Task<IActionResult> DetalleAlbum(uint id){
        
        var album = await repoAlbum.DetalleDe(id);

        return View(album);
    }
    }
}