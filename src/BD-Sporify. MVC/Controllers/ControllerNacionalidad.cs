using Microsoft.AspNetCore.Mvc;
using BD_Sporify._MVC.Models;
using Spotify.Core;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;



namespace SpotifyMVC.Controllers
{
    public class NacionalidadController : Controller
    {
        private readonly ILogger<NacionalidadController> _logger;
        private readonly IRepoNacionalidadAsync repoNacionalidad;

        public NacionalidadController(
            ILogger<NacionalidadController> logger,
            IRepoNacionalidadAsync repoNacionalidad)
        {
            _logger = logger;
            this.repoNacionalidad = repoNacionalidad;

        }

        // GET: mostrar formulario
        public async Task<IActionResult> Index()
        {

            var tipoSuscripciones = await repoNacionalidad.Obtener();

            return View(tipoSuscripciones);
        }
        public ActionResult CreateNacionalidad() => View();

        [HttpPost]
        public async Task<IActionResult> CreateNacionalidad(Nacionalidad nacionalidad)
        {
            await repoNacionalidad.Alta(nacionalidad);
            return RedirectToAction("Index");
        }
    
        [HttpGet]
        public async Task<IActionResult> DetalleDeNacionalidad(uint id){
            
            var nacionalidad = await repoNacionalidad.DetalleDe(id);

            return View(nacionalidad);
        }
        }
}