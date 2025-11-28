using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using BD_Sporify._MVC.Models;
using Spotify.Core.Persistencia;
using Spotify.ReposDapper;
using Spotify.Core;

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

        return View(artistas);
    }

    [HttpGet]
    public IActionResult CreateArtista() => View();
    // POST: Artista/Index (para dar de alta un artista)

    [HttpPost]
    public async Task<IActionResult> CreateArtista(Artista artista)
    {
        List<Artista> artistas = await repoArtista.Obtener();
        if (artistas.Where(x => x.NombreArtistico == artista.NombreArtistico).Any())
        {
            ViewBag.Error = "Ya existe un artista con este Nombre Artistico.";
            return View();
        }

        var altaArtista = await repoArtista.Alta(artista);

        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> DetalleDeArtista(uint id)
    {
        var artista = await repoArtista.DetalleDe(id);

        if (artista == null)
            return NotFound();

        return View(artista); 
    }


}
