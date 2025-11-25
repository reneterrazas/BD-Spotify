using Microsoft.AspNetCore.Mvc;
using BD_Sporify._MVC.Models;
using Spotify.Core;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BD_Sporify._MVC.Controllers
{
    public class AuthController : Controller
    {
        private readonly IRepoUsuarioAsinc _repoUsuario;

        // Inyectamos el repositorio en el constructor
        public AuthController(IRepoUsuarioAsinc repoUsuario)
        {
            _repoUsuario = repoUsuario;
        }

        // GET: /Auth/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Auth/Login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Buscamos el usuario en la base de datos
            var usuario = await _repoUsuario.Login(model.Email, model.Contrasenia);

            if (usuario != null)
            {
                // Guardamos sesión
                HttpContext.Session.SetString("UsuarioLogueado", usuario.Email);

                return RedirectToAction("Index", "Home");
            }

            // Si está mal
            ViewBag.Error = "Email o contraseña incorrectos.";
            return View(model);
        }

        // GET: /Auth/Register
 public async Task<IActionResult> Register()
{
    var nacionalidades = await _repoUsuario.RepoNacionalir(); // List<Nacionalidad>

    var model = new RegisterViewModel
    {
        Nacionalidades = new SelectList(nacionalidades, "IdNacionalidad", "Nombre")
    };

    return View(model);
}

        // POST: /Auth/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Verificar que las contraseñas coincidan
            if (model.Contrasenia != model.ConfirmarContrasenia)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View(model);
            }

            // Verificar si ya existe un usuario con ese email
            var usuarioExistente = await _repoUsuario.GetUsuarioByEmailAsync(model.Email);
            if (usuarioExistente != null)
            {
                ViewBag.Error = "Ya existe un usuario con este email.";
                return View(model);
            }

            // Crear usuario
            var nuevoUsuario = new Usuario
            {
                NombreUsuario = model.Nombre,
                Email = model.Email,
                Contrasenia = model.Contrasenia, // Opcional: aplicar hash aquí
                nacionalidad = new(){ idNacionalidad = (uint)Convert.ToInt32(model.NacionalidadId), Pais = new()}
            };

            await _repoUsuario.AltaUsuarioAsync(nuevoUsuario);

            // Redirigir al login
            return RedirectToAction("Login");
        }

        // Cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}