using Microsoft.AspNetCore.Mvc;
using BD_Sporify._MVC.Models;
using Spotify.Core;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;



namespace SpotifyMVC.Controllers
{
    public class TipoSuscripcionController : Controller
    {
        private readonly ILogger<TipoSuscripcionController> _logger;
        private readonly IRepoTipoSuscripcionAsync repoTipo;

        public TipoSuscripcionController(
            ILogger<TipoSuscripcionController> logger,
            IRepoTipoSuscripcionAsync repoTipo)
        {
            _logger = logger;
            this.repoTipo = repoTipo;

        }

        // GET: mostrar formulario
        public async Task<IActionResult> Index()
        {

            var tipoSuscripciones = await repoTipo.Obtener();

            return View(tipoSuscripciones);
        }
        public ActionResult CreateTipoSuscripcion() => View();

        [HttpPost]
        public async Task<IActionResult> CreateTipoSuscripcion(TipoSuscripcion tipoSuscripcion)
        {
            await repoTipo.Alta(tipoSuscripcion);
            return RedirectToAction("Index");
        }
    
        [HttpGet]
        public async Task<IActionResult> DetalleTipoSuscripcion(uint id){
            
            var tipoSuscripcion = await repoTipo.DetalleDe(id);

            return View(tipoSuscripcion);
        }
        }
}