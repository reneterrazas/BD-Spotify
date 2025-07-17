namespace Spotify.ReposDapper.Test;

public class RepoTipoSuscripcionTest : TestBase
{
    private RepoTipoSuscripcion _repoTipoSuscripcion;

    public RepoTipoSuscripcionTest() : base()
    {
        this._repoTipoSuscripcion = new RepoTipoSuscripcion(Conexion);
    }

    [Fact]
    public void ListarOk()
    {
        var ListaTiposSuscripcion = _repoTipoSuscripcion.Obtener();

        Assert.NotEmpty(ListaTiposSuscripcion);
        Assert.NotNull(ListaTiposSuscripcion);
    }

    [Fact]
    public void AltaTipoSuscripcion()
    {
        var tipoSuscripcion = new TipoSuscripcion
        {
            Duracion = 4,
            Costo = 23,
            Tipo = "Anual"
        };

        var IdTipoSuscripcionCreado = _repoTipoSuscripcion.Alta(tipoSuscripcion);
        
        var ListaTiposSuscripcion = _repoTipoSuscripcion.Obtener();

        Assert.Contains(ListaTiposSuscripcion, variable => variable.IdTipoSuscripcion == IdTipoSuscripcionCreado);
    }
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public void DetalleIdTipoSuscripcion(uint idTipoSuscripcion)
    {
        var tipoSuscripcionPorId = _repoTipoSuscripcion.DetalleDe(idTipoSuscripcion);

        Assert.NotNull(tipoSuscripcionPorId);
        Assert.Equal(idTipoSuscripcion, tipoSuscripcionPorId.IdTipoSuscripcion);
    }
    [Fact]
    public async Task ListarAsync_OK()
    {
        var repo = new RepoTipoSuscripcion(Conexion);
        var tipos = await repo.ObtenerAsync();
        Assert.NotNull(tipos);
        Assert.NotEmpty(tipos);
    }

    [Fact]
    public async Task AltaTipoSuscripcionAsync_Ok()
    {
        var repo = new RepoTipoSuscripcion(Conexion);
        var tipo = new TipoSuscripcion { Costo = 100, Duracion = 30, Tipo = "Async" };
        var id = await repo.AltaAsync(tipo);
        var tipos = await repo.ObtenerAsync();
        Assert.Contains(tipos, t => t.IdTipoSuscripcion == id);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task DetalleDeAsync_OK(uint idTipoSuscripcion)
    {
        var repo = new RepoTipoSuscripcion(Conexion);
        var tipo = await repo.DetalleDeAsync(idTipoSuscripcion);
        Assert.NotNull(tipo);
        Assert.Equal(idTipoSuscripcion, tipo.IdTipoSuscripcion);
    }
    
}
