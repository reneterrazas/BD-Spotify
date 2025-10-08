using Microsoft.AspNetCore.Mvc;
using BD_Sporify._MVC.Models;
using Spotify.Core;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;



namespace SpotifyMVC.Controllers
{
    public class GeneroController : Controller
    {
        private readonly ILogger<GeneroController> _logger;
        private readonly IRepoGeneroAsync repoGenero;

        public GeneroController(
            ILogger<GeneroController> logger,
            IRepoGeneroAsync repoGenero
            )
        {
            _logger = logger;
            this.repoGenero = repoGenero;
        }

        // GET: mostrar formulario
        public async Task<IActionResult> Index()
        {

            var generos = await repoGenero.Obtener();

            return View(generos);
        }

        public IActionResult CreateGenero() => View();

        [HttpPost]
        public async Task<IActionResult> CreateGenero(Genero a)
        {

            await repoGenero.Alta(a);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DetalleDeGenero(byte id){
            
            var generoDetallado = await repoGenero.DetalleDe(id);

            return View(generoDetallado);
        }
    }
}