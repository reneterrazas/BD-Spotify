using Microsoft.AspNetCore.Mvc;
using BD_Sporify._MVC.Models;
using Spotify.Core;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;

namespace BD_Sporify._MVC.Controllers
{
    public class AuthController : Controller
    {
        // GET: /Auth/Index
        public IActionResult Index()
        {
            return View();
        }

        // POST: /Auth/Index
        [HttpPost]
        public IActionResult Index(LoginViewModel model)
        {
            // Validación fija
            string userEmail = "admin@gmail.com";
            string userPass = "123";

            if (model.Email == userEmail && model.Contrasenia == userPass)
            {
                // Guardamos sesión
                HttpContext.Session.SetString("UsuarioLogueado", userEmail);

                return RedirectToAction("Index", "Home");
            }

            // Si está mal
            ViewBag.Error = "Email o contraseña incorrectos.";
            return View(model);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Auth");
        }
    }
}