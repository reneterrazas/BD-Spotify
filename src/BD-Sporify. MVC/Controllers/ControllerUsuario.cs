using Microsoft.AspNetCore.Mvc;
using BD_Sporify._MVC.Models;
using Spotify.Core;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;



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
                    Email = model.Email,
                    Contrasenia = model.Contraseña,
                    nacionalidad = nacionalidadSeleccionada
                };
                            var usuarioExistente = await repoUsuario.Obtener();
            if (usuarioExistente.Where(x => x.Email == model.Email).Any())
                {
                ViewBag.Error = "Ya existe un usuario con este email.";
            model.nacionalidades = await repoNacionalidad.Obtener();

                    return View(model);
            }

                await repoUsuario.Alta(usuario);
                return RedirectToAction("Index");
            }

            model.nacionalidades = await repoNacionalidad.Obtener();
            return View(model);

        }


        [HttpGet]
        public async Task<IActionResult> DetalleUsuario(uint id) {

            var usuario = await repoUsuario.DetalleDe(id);
            


            if (string.IsNullOrEmpty(usuario.Email))
            {
                throw new Exception("No hay email");
            }

        return View(usuario);
    }
    [HttpGet]
    public IActionResult Login()
    {
    return View();
    }
    [HttpPost]
    public async Task<IActionResult> Login(string email, string contrasenia)
    {
    var usuario = await repoUsuario.Login(email, contrasenia);

    if (usuario == null)
    {
        ViewBag.Error = "Email o contraseña incorrectos.";
        return View();
    }

    // Crear claims
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, usuario.NombreUsuario),
        new Claim("Email", usuario.Email)
    };

    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var principal = new ClaimsPrincipal(identity);

    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

    return RedirectToAction("Index", "Home");
    }


    }
}