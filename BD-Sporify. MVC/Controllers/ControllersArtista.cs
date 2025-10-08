using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BD_Sporify._MVC.Models;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;

public class ArtistaController : Controller
{
    private readonly IRepoArtistaAsync repoArtista;
    private readonly ILogger<ArtistaController> _logger;

    public ArtistaController(IRepoArtistaAsync repoArtista, ILogger<ArtistaController> logger)
    {
        this.repoArtista = repoArtista;
        _logger = logger;
    }

    // GET: Artista/Index
    public async Task<IActionResult> Index()
    {
        var artistas = await repoArtista.Obtener();

        var viewModel = new ArtistaViewModel
        {
            artistas = artistas
        };

        return View(viewModel);
    }

    [HttpGet]
    public IActionResult CreateArtista() => View();
    // POST: Artista/Index (para dar de alta un artista)
    
    [HttpPost]
    public async Task<IActionResult> CreateArtista(ArtistaViewModel model)
    {
        
       var altaArtista = await repoArtista.Alta(model.artista);
        model.artistas = await repoArtista.Obtener();

        return View(model);
    }
    // GET: Artista/Detalle/5
public async Task<IActionResult> Detalle(int id)
{
    var artista = await repoArtista.DetalleDe((uint)id);

    if (artista == null)
    {
        return NotFound(); 
    }

    return View(artista);
}

}
