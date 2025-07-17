namespace Spotify.ReposDapper.Test;

public class RepoCancionTest : TestBase
{
    private RepoAlbum _repoAlbum;
    private RepoGenero _repoGenero;
    private RepoArtista _repoArtista;
    private RepoCancion _repoCancion;
    public RepoCancionTest() : base()
    {
        this._repoCancion = new RepoCancion(Conexion);
        this._repoArtista = new RepoArtista(Conexion);
        this._repoAlbum = new RepoAlbum(Conexion);
        this._repoGenero = new RepoGenero(Conexion);
    }

    [Fact]
    public void ListarOk()
    {
        var ListaCanciones = _repoCancion.Obtener();

        Assert.NotNull(ListaCanciones);
    }

    [Fact]
    public void AltaCancion()
    {
        var unArtista = _repoArtista.Obtener().First();
        var unAlbum = _repoAlbum.Obtener().First();
        var unGenero = _repoGenero.Obtener().First();

        var InsertarCancion = new Cancion
        {
            Titulo = "Lamento ser boliviano",
            Duracion = new TimeSpan(0, 15, 2),
            album = unAlbum,
            genero = unGenero,
            artista = unArtista
        };

        var idCancionInsertada = _repoCancion.Alta(InsertarCancion);

        var ListaCanciones = _repoCancion.Obtener();

        Assert.Contains(ListaCanciones, variable => variable.idCancion == idCancionInsertada);

    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]

    public void DetallaIdCanciones(uint idCancion)
    {
        var BuscarIdCanciones = _repoCancion.DetalleDe(idCancion);

        Assert.NotNull(BuscarIdCanciones);
        Assert.Equal(idCancion, BuscarIdCanciones.idCancion);
    }


    [Theory]
    [InlineData("Re")]
    [InlineData("Co")]

    public void MatcheoDeTituloPorTitulo(string cadena)
    {
        var CancionesTitulo = _repoCancion.Matcheo(cadena);

        foreach (var titulo in CancionesTitulo)
        {
            Console.WriteLine(titulo);
        }
    }
     [Fact]
    public async Task ListarAsync_OK()
    {
        var repo = new RepoCancion(Conexion);
        var canciones = await repo.ObtenerAsync();
        Assert.NotNull(canciones);
        Assert.NotEmpty(canciones);
    }

    [Fact]
    public async Task AltaCancionAsync_Ok()
    {
        var repo = new RepoCancion(Conexion);
        var cancion = new Cancion
        { Titulo = "Async Song",
          Duracion = new TimeSpan(0,12,56),
          album = new Album { idAlbum = 1 },
          artista = new Artista { idArtista = 1, NombreArtistico = "Artista Test" },
          genero = new Genero { idGenero = 1 } };
        var id = await repo.AltaAsync(cancion);
        var canciones = await repo.ObtenerAsync();
        Assert.Contains(canciones, c => c.idCancion == id);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task DetalleDeAsync_OK(uint idCancion)
    {
        var repo = new RepoCancion(Conexion);
        var cancion = await repo.DetalleDeAsync(idCancion);
        Assert.NotNull(cancion);
        Assert.Equal(idCancion, cancion.idCancion);
    }

    [Fact]
    public async Task MatcheoAsync_OK()
    {
        var repo = new RepoCancion(Conexion);
        var resultado = await repo.MatcheoAsync("Celos");
        Assert.NotNull(resultado);
        Assert.Contains(resultado, t => t.Contains("Celos"));
    }
}

