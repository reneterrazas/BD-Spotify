using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Spotify.ReposDapper.Test
{
    public class RepoAlbumTestAsync : TestBase
    {
    [Fact]
    public async Task AltaAlbumAsync_Ok()
    {
        var repo = new RepoAlbumAsync(Conexion);
        var album = new Album { Titulo = "Nuevo Album Async", artista = new Artista { idArtista = 1, NombreArtistico = "Artista Test", Nombre = "Nombre Test", Apellido = "Apellido Test" } };
        var id = await repo.Alta(album);
        var albums = await repo.Obtener();
        Assert.Contains(albums, a => a.idAlbum == id);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task DetalleDeAsync_OK(uint idAlbum)
    {
        var repo = new RepoAlbumAsync(Conexion);
        var album = await repo.DetalleDe(idAlbum);
        Assert.NotNull(album);
        Assert.Equal(idAlbum, album.idAlbum);
    }

    [Fact]
    public async Task EliminarAsync_OK()
    {
        var repo = new RepoAlbumAsync(Conexion);
        var album = new Album { Titulo = "Para Eliminar Async", artista = new Artista { idArtista = 1, NombreArtistico = "Artista Test", Nombre = "Nombre Test", Apellido = "Apellido Test" } };
        var id = await repo.Alta(album);
        await repo.Eliminar(id);
        var albumEliminado = await repo.DetalleDe(id);
        Assert.Null(albumEliminado);
    }
    }
}