using Microsoft.AspNetCore.Mvc;
using BD_Sporify._MVC.Models;
using Spotify.Core;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;

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
            {
                return View(model);
            }

            // Buscamos el usuario en la base de datos por email
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

        // Cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Auth");
        }
    }
}