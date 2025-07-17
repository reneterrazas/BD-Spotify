namespace Spotify.ReposDapper.Test;

public class RepoReproduccionTests : TestBase
{
    private RepoUsuario _repoUsuario;
    private RepoCancion _repoCancion;
    private RepoReproduccion _repoReproduccion;

    public RepoReproduccionTests() : base()
    {
        this._repoReproduccion = new RepoReproduccion(Conexion);
        this._repoUsuario = new RepoUsuario(Conexion);
        this._repoCancion = new RepoCancion(Conexion);
    }

    [Fact]

    public void ListarOk()
    {
        var ListaDeReproducciones = _repoReproduccion.Obtener();

        Assert.NotNull(ListaDeReproducciones);
        Assert.NotEmpty(ListaDeReproducciones);
    }

    [Fact]
    public void AltaReproduccion()
    {
        var unUsuario = _repoUsuario.Obtener().First();
        var unaCancion = _repoCancion.Obtener().First();

        var CrearReproduccion = new Reproduccion
        {
            usuario = unUsuario,
            cancion = unaCancion,
            FechaReproduccion = DateTime.Now
        };

        var idCreadoReproduccion = _repoReproduccion.Alta(CrearReproduccion);
        var ListadoDeReproducciones = _repoReproduccion.Obtener();

        Assert.Contains(ListadoDeReproducciones, variable => variable.IdHistorial == idCreadoReproduccion);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]

    public void DetalleIdReproduccion(uint idReproduccion)
    {
        var ReproduccionPorId = _repoReproduccion.DetalleDe(idReproduccion);

        Assert.NotNull(ReproduccionPorId);
        Assert.Equal(idReproduccion, ReproduccionPorId.IdHistorial);
    }
     [Fact]
    public async Task ListarAsync_OK()
    {
        var repo = new RepoReproduccion(Conexion);
        var reproducciones = await repo.ObtenerAsync();
        Assert.NotNull(reproducciones);
        Assert.NotEmpty(reproducciones);
    }

    [Fact]
    public async Task AltaReproduccionAsync_Ok()
    {
        var repo = new RepoReproduccion(Conexion);
        var reproduccion = new Reproduccion { usuario = new Usuario { idUsuario = 1 }, cancion = new Cancion { idCancion = 1 }, FechaReproduccion = DateTime.Now };
        var id = await repo.AltaAsync(reproduccion);
        var reproducciones = await repo.ObtenerAsync();
        Assert.Contains(reproducciones, r => r.IdHistorial == id);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task DetalleDeAsync_OK(uint idHistorial)
    {
        var repo = new RepoReproduccion(Conexion);
        var reproduccion = await repo.DetalleDeAsync(idHistorial);
        Assert.NotNull(reproduccion);
        Assert.Equal(idHistorial, reproduccion.IdHistorial);
    }
}
