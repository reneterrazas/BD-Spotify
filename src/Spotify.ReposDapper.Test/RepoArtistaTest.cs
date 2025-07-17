namespace Spotify.ReposDapper.Test;

public class RepoArtistaTest : TestBase
{
    private RepoArtista _repoArtista;
    public RepoArtistaTest() : base("MySQL")
    {
        this._repoArtista = new RepoArtista(Conexion);
    }

    [Fact]
    public void ListarOk()
    {
        var ListaArtistas = _repoArtista.Obtener();

        Assert.NotNull(ListaArtistas);
    }

    [Fact]
    public void Alta()
    {
        var ArtistaInsertar = new Artista
        {
            NombreArtistico = "MARIO BUSTAMAN",
            Nombre = "Mario",
            Apellido = "Rojas Villalva"
        };

        var idArtistaInsertado = _repoArtista.Alta(ArtistaInsertar);

        var ListaArtistas = _repoArtista.Obtener();

        Assert.Contains(ListaArtistas, variable => variable.Nombre == "Mario");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]

    public void DetalleArtistaPorId(uint idArtista)
    {
        var ArtistaPorId = _repoArtista.DetalleDe(idArtista);

        Assert.NotNull(ArtistaPorId);
        Assert.Equal(idArtista , ArtistaPorId.idArtista);
    }
    [Fact]
    public async Task ListarAsync_OK()
    {
        var repo = new RepoArtista(Conexion);
        var artistas = await repo.ObtenerAsync();
        Assert.NotNull(artistas);
        Assert.NotEmpty(artistas);
    }

    [Fact]
    public async Task AltaArtistaAsync_Ok()
    {
        var repo = new RepoArtista(Conexion);
        var artista = new Artista { NombreArtistico = "Async Artista", Nombre = "Nombre", Apellido = "Apellido" };
        var id = await repo.AltaAsync(artista);
        var artistas = await repo.ObtenerAsync();
        Assert.Contains(artistas, a => a.idArtista == id);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task DetalleDeAsync_OK(uint idArtista)
    {
        var repo = new RepoArtista(Conexion);
        var artista = await repo.DetalleDeAsync(idArtista);
        Assert.NotNull(artista);
        Assert.Equal(idArtista, artista.idArtista);
    }

    [Fact]
    public async Task EliminarAsync_OK()
    {
        var repo = new RepoArtista(Conexion);
        var artista = new Artista { NombreArtistico = "Para Eliminar Async", Nombre = "Nombre", Apellido = "Apellido" };
        var id = await repo.AltaAsync(artista);
        await repo.EliminarAsync(id);
        var artistaEliminado = await repo.DetalleDeAsync(id);
        Assert.Null(artistaEliminado);
    }
}
