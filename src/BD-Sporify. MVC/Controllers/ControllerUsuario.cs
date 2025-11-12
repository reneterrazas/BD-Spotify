using Microsoft.AspNetCore.Mvc;
using BD_Sporify._MVC.Models;
using Spotify.Core;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;



namespace SpotifyMVC.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly ILogger<UsuarioController> _logger;
        private readonly IRepoNacionalidadAsync repoNacionalidad;
        private readonly IRepoUsuarioAsinc repoUsuario;

        public UsuarioController(
            ILogger<UsuarioController> logger,
            IRepoNacionalidadAsync repoNacionalidad,
            IRepoUsuarioAsinc repoUsuario)
        {
            _logger = logger;
            this.repoNacionalidad = repoNacionalidad;
            this.repoUsuario = repoUsuario;
        }

        // GET: mostrar formulario
        public async Task<IActionResult> Index()
        {

            var usuarios = await repoUsuario.Obtener();

            return View(usuarios);
        }
        [HttpGet]
        public async Task<IActionResult> CreateUsuario(){
            
            var vm = new UsuarioViewModel{
                nacionalidades = await repoNacionalidad.Obtener()
            };

            return View(vm);
        }

        // POST: dar de alta usuario
        [HttpPost]
        public async Task<IActionResult> CreateUsuario(UsuarioViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Traer la nacionalidad  por su ID
                var nacionalidadSeleccionada = await repoNacionalidad.DetalleDe(model.NacionalidadId);

                // Crear el usuario usando el objeto nacionalidada
                var usuario = new Usuario
                {
                    NombreUsuario = model.NombreUsuario,
                    Gmail = model.Email,
                    Contrasenia = model.Contraseña,
                    nacionalidad = nacionalidadSeleccionada
                };

                await repoUsuario.Alta(usuario);
                return RedirectToAction("Index");
            }

            model.nacionalidades = await repoNacionalidad.Obtener();
            return View(model);
        }

    
    [HttpGet]
    public async Task<IActionResult> DetalleUsuario(uint id){
        
        var usuario = await repoUsuario.DetalleDe(id);

        return View(usuario);
    }
    }
}