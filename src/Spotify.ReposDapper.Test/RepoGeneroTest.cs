namespace Spotify.ReposDapper.Test;

public class RepoGeneroTest : TestBase
{
    RepoGenero _repoGenero;

    public RepoGeneroTest() : base()
        => _repoGenero = new RepoGenero(Conexion);

    [Fact]
    public void ListarOK()
    {
        var generos = _repoGenero.Obtener();
        Assert.NotEmpty(generos);
    }

    [Fact]
    public void AltaGeneroOK()
    {
        var nuevoGenero = new Genero { genero = "Rock" };
        var IdGenero = _repoGenero.Alta(nuevoGenero);

        var generos = _repoGenero.Obtener();
        Assert.Contains(generos, g => g.idGenero == IdGenero);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]

    public void DetalleGeneroPorId(byte idGenero)
    {
        var BuscarGenero = _repoGenero.DetalleDe(idGenero);

        Assert.NotNull(BuscarGenero);
        Assert.Equal(idGenero, BuscarGenero.idGenero);
    }

    /*
    [Fact]

    public void EleminarGeneroOK()
    {
        var generos = _repoGenero.Obtener();
        var generoAEliminar = generos.FirstOrDefault(g => g.genero == "Rock");

        if (generoAEliminar != null)
        {
            _repoGenero.Eliminar(generoAEliminar.idGenero);
            generos = _repoGenero.Obtener();
            Assert.DoesNotContain(generos, g => g.genero == "Rock");
        }
        
    }
    */
    [Fact]
    public async Task ListarAsync_OK()
    {
        var repo = new RepoGenero(Conexion);
        var generos = await repo.ObtenerAsync();
        Assert.NotNull(generos);
        Assert.NotEmpty(generos);
    }

    [Fact]
    public async Task AltaGeneroAsync_Ok()
    {
        var repo = new RepoGenero(Conexion);
        var genero = new Genero { genero = "Async Genero" };
        var id = await repo.AltaAsync(genero);
        var generos = await repo.ObtenerAsync();
        Assert.Contains(generos, g => g.idGenero == id);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task DetalleDeAsync_OK(byte idGenero)
    {
        var repo = new RepoGenero(Conexion);
        var genero = await repo.DetalleDeAsync(idGenero);
        Assert.NotNull(genero);
        Assert.Equal(idGenero, genero.idGenero);
    }
}
