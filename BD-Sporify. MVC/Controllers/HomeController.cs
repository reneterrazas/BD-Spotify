using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BD_Sporify._MVC.Models;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;



public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IRepoArtistaAsync repoArtista;
    public HomeController(ILogger<HomeController> logger,IRepoArtistaAsync repoArtista)
    {
        this.repoArtista = repoArtista;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {   
        var artistas = await repoArtista.Obtener();
        var ArtistaVidewModels = new ArtistaViewModel();
        ArtistaVidewModels.artistas = artistas;
        return View(ArtistaVidewModels);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [HttpPost]


    public async Task<IActionResult> Index(ArtistaViewModel model)


    {

        var altaArtista = await repoArtista.Alta(model.artista);
        model.artistas = await repoArtista.Obtener();

        return View(model);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
