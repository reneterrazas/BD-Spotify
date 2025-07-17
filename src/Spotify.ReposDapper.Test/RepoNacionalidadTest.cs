namespace Spotify.ReposDapper.Test;

public class RepoNacionalidadTest : TestBase
{
    RepoNacionalidad _repoNacionalidad;

    public RepoNacionalidadTest() : base()
    {
        _repoNacionalidad = new RepoNacionalidad(Conexion);
    }

    [Fact]
    public void ListarOK()
    {
        var nacionalidades = _repoNacionalidad.Obtener();

        Assert.Contains(nacionalidades, n => n.Pais == "Argentina");
    }

    [Fact]
    public void AltaNacionalidadOK()
    {
        var nuevaNacionalidad = new Nacionalidad { Pais = "España" };
        var IdNAcionalidad = _repoNacionalidad.Alta(nuevaNacionalidad);

        var nacionalidades = _repoNacionalidad.Obtener();

        Assert.Contains(nacionalidades, n => n.Pais == "España");
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]

    public void DetalleIdNacionalidad(uint idNacionalidad)
    {
        var BuscarNacionalidadPorId = _repoNacionalidad.DetalleDe(idNacionalidad);

        Assert.NotNull(BuscarNacionalidadPorId);
        Assert.Equal(idNacionalidad, BuscarNacionalidadPorId.idNacionalidad);
    }
    [Fact]
    public async Task ListarAsync_OK()
    {
        var repo = new RepoNacionalidad(Conexion);
        var nacionalidades = await repo.ObtenerAsync();
        Assert.NotNull(nacionalidades);
        Assert.NotEmpty(nacionalidades);
    }

    [Fact]
    public async Task AltaNacionalidadAsync_Ok()
    {
        var repo = new RepoNacionalidad(Conexion);
        var nacionalidad = new Nacionalidad { Pais = "Async País" };
        var id = await repo.AltaAsync(nacionalidad);
        var nacionalidades = await repo.ObtenerAsync();
        Assert.Contains(nacionalidades, n => n.idNacionalidad == id);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task DetalleDeAsync_OK(uint idNacionalidad)
    {
        var repo = new RepoNacionalidad(Conexion);
        var nacionalidad = await repo.DetalleDeAsync(idNacionalidad);
        Assert.NotNull(nacionalidad);
        Assert.Equal(idNacionalidad, nacionalidad.idNacionalidad);
    }
}
