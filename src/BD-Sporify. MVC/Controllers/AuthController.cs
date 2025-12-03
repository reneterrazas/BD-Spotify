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
        private readonly IRepoNacionalidadAsync _repoNacionalidad;

        // Inyectamos el repositorio en el constructor
        public AuthController(IRepoUsuarioAsinc repoUsuario, IRepoNacionalidadAsync repoNacionalidad)
        {
            _repoNacionalidad = repoNacionalidad;
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
                if(usuario.Email.Contains("rene"))
                {
                    HttpContext.Session.SetString("Rol", "Admin");
                }
                else
                {
                    HttpContext.Session.SetString("Rol", "Usuario");
                }

                return RedirectToAction("Index", "Home");
            }

            // Si está mal
            ViewBag.Error = "Email o contraseña incorrectos.";
            return View(model);
        }

        // GET: /Auth/Register
 public async Task<IActionResult> Register()
{
    var nacionalidades = await _repoNacionalidad.Obtener(); // List<Nacionalidad>

    var model = new RegisterViewModel
    {
        Nacionalidades = new SelectList(nacionalidades, nameof(Nacionalidad.idNacionalidad), nameof(Nacionalidad.Pais))
    };

    return View(model);
}

        // POST: /Auth/Register
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            var nacionalidades = await _repoNacionalidad.Obtener(); // List<Nacionalidad>
            model.Nacionalidades = new SelectList(nacionalidades, nameof(Nacionalidad.idNacionalidad), nameof(Nacionalidad.Pais));

            if (!ModelState.IsValid)
                return View(model);
            

            // Verificar que las contraseñas coincidan
            if (model.Contrasenia != model.ConfirmarContrasenia)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                return View(model);
            }

            // Verificar si ya existe un usuario con ese email
            var usuarioExistente = await _repoUsuario.Obtener();
            if (usuarioExistente.Where(x => x.Email == model.Email).Any())
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
                nacionalidad = new(){ idNacionalidad = (uint)Convert.ToInt32(model.NacionalidadId), Pais = ""}
            };

            await _repoUsuario.Alta(nuevoUsuario);

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