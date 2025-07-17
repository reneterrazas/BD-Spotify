namespace Spotify.ReposDapper.Test;

public class RepoSuscripcionTest : TestBase
{
    private RepoUsuario _repoUsuario;
    private RepoTipoSuscripcion _repoTipoSuscripcion;
    private RepoSuscripcion _repoSuscripcion;
    public RepoSuscripcionTest() : base()
    {
        this._repoSuscripcion = new RepoSuscripcion(Conexion);
        this._repoTipoSuscripcion = new RepoTipoSuscripcion(Conexion);
        this._repoUsuario = new RepoUsuario(Conexion);
    }

    [Fact]
    public void ListarOk()
    {
        var ListaSuscripciones = _repoSuscripcion.Obtener();

        Assert.NotNull(ListaSuscripciones);
        Assert.NotEmpty(ListaSuscripciones);
    }

    [Fact]
    public void AltaSuscripcion()
    {
        var unUsuario = _repoUsuario.Obtener().First();
        var unTipoSuscripcion = _repoTipoSuscripcion.Obtener().First();

        var suscripcion = new Registro
        {
            usuario = unUsuario,
            tipoSuscripcion = unTipoSuscripcion,
            FechaInicio = DateTime.Now
        };

        var idSuscripcionCreada = _repoSuscripcion.Alta(suscripcion);
        var ListaRegistroDeSuscripciones = _repoSuscripcion.Obtener();

        Assert.Contains(ListaRegistroDeSuscripciones, variable => variable.idSuscripcion == idSuscripcionCreada);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]

    public void DetallePorIdDeSuscripcion(uint idSuscripcion)
    {
        var SuscripcionPorId = _repoSuscripcion.DetalleDe(idSuscripcion);

        Assert.NotNull(SuscripcionPorId);
        Assert.Equal(idSuscripcion, SuscripcionPorId.idSuscripcion);
    }
     [Fact]
    public async Task ListarAsync_OK()
    {
        var repo = new RepoSuscripcion(Conexion);
        var suscripciones = await repo.ObtenerAsync();
        Assert.NotNull(suscripciones);
        Assert.NotEmpty(suscripciones);
    }

    [Fact]
    public async Task AltaSuscripcionAsync_Ok()
    {
        var repo = new RepoSuscripcion(Conexion);
        var registro = new Registro { usuario = new Usuario { idUsuario = 1 }, tipoSuscripcion = new TipoSuscripcion { IdTipoSuscripcion = 1 }, FechaInicio = DateTime.Now };
        var id = await repo.AltaAsync(registro);
        var suscripciones = await repo.ObtenerAsync();
        Assert.Contains(suscripciones, s => s.idSuscripcion == id);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task DetalleDeAsync_OK(uint idSuscripcion)
    {
        var repo = new RepoSuscripcion(Conexion);
        var registro = await repo.DetalleDeAsync(idSuscripcion);
        Assert.NotNull(registro);
        Assert.Equal(idSuscripcion, registro.idSuscripcion);
    }
}
