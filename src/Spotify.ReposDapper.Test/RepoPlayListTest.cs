using System;
using System.Collections.Generic;
using System.Linq;

namespace Spotify.ReposDapper.Test;
public class RepoPlayListTest : TestBase
{
    RepoPlaylist _repoPlayList;
    RepoUsuario _repoUsuario;
    RepoCancion _repoCancion;
    
    public RepoPlayListTest() : base()
    {
        _repoPlayList = new RepoPlaylist(Conexion);
        _repoUsuario = new RepoUsuario(Conexion);
        _repoCancion = new RepoCancion(Conexion);
    } 


    [Fact]
    public void ListarOK()
    {
        var playLists = _repoPlayList.Obtener();

        Assert.NotNull(playLists);
        Assert.NotEmpty(playLists);
    }

    [Fact]
    public void AltaPlaylistOk()
    {
        var MuchasCanciones = _repoCancion.Obtener().ToList();
        var unUsuario = _repoUsuario.Obtener().First(); 

        var Playlist_a_insertar = new PlayList 
        {
            Nombre = "Tus labios me tocan",
            usuario = unUsuario,
            Canciones = MuchasCanciones
        };

        var idPlayListAdarAlta = _repoPlayList.Alta(Playlist_a_insertar);

        var ListaPlaylists = _repoPlayList.Obtener();

        Assert.Contains(ListaPlaylists, variable => variable.idPlaylist == idPlayListAdarAlta);
    }

        [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]

    public void DetalleIdPlaylist(uint idPlaylist)
    {
        var PlayListPorId = _repoPlayList.DetalleDe(idPlaylist);

        Assert.NotNull(PlayListPorId);
        Assert.Equal(idPlaylist , PlayListPorId.idPlaylist);
    }

    [Fact]
    public void DetallePlaylistOk()
    {
        // Act
        var cancionesObtenidas = _repoPlayList.DetallePlaylist(8);

        // Assert
        Assert.NotNull(cancionesObtenidas);
        Assert.Contains(cancionesObtenidas, c => c.Titulo == "Estos Celos");
    }
               
    [Fact]
    public void DetallePlaylistNoExistente()
    {
        // Act
        var cancionesObtenidas = _repoPlayList.DetallePlaylist(11);

        // Ass  ert
        Assert.Null(cancionesObtenidas);
    }

    [Fact]
    public void DetallePlaylistVacia()
    {
        // Act
        var cancionesObtenidas = _repoPlayList.DetallePlaylist(2);

        // Assert
        Assert.NotNull(cancionesObtenidas);
        Assert.Empty(cancionesObtenidas); 
    }
        [Fact]
    public async Task ListarAsync_OK()
    {
        var playLists = await _repoPlayList.ObtenerAsync();
        Assert.NotNull(playLists);
        Assert.NotEmpty(playLists);
    }

    [Fact]
    public async Task AltaPlaylistAsync_Ok()
    {
        var MuchasCanciones = (await _repoCancion.ObtenerAsync()).ToList();
        var unUsuario = (await _repoUsuario.ObtenerAsync()).First();

        var Playlist_a_insertar = new PlayList
        {
            Nombre = "Tus labios me tocan (async)",
            usuario = unUsuario,
            Canciones = MuchasCanciones
        };

        var idPlayListAdarAlta = await _repoPlayList.AltaAsync(Playlist_a_insertar);
        var ListaPlaylists = await _repoPlayList.ObtenerAsync();
        Assert.Contains(ListaPlaylists, variable => variable.idPlaylist == idPlayListAdarAlta);
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    public async Task DetalleIdPlaylistAsync(uint idPlaylist)
    {
        var PlayListPorId = await _repoPlayList.DetalleDeAsync(idPlaylist);
        Assert.NotNull(PlayListPorId);
        Assert.Equal(idPlaylist, PlayListPorId.idPlaylist);
    }

    [Fact]
    public async Task DetallePlaylistAsync_Ok()
    {
        var cancionesObtenidas = await _repoPlayList.DetallePlaylistAsync(8);
        Assert.NotNull(cancionesObtenidas);
        Assert.Contains(cancionesObtenidas, c => c.Titulo == "Estos Celos");
    }

    [Fact]
    public async Task DetallePlaylistAsync_NoExistente()
    {
        var cancionesObtenidas = await _repoPlayList.DetallePlaylistAsync(11);
        Assert.Null(cancionesObtenidas);
    }

    [Fact]
    public async Task DetallePlaylistAsync_Vacia()
    {
        var cancionesObtenidas = await _repoPlayList.DetallePlaylistAsync(2);
        Assert.NotNull(cancionesObtenidas);
        Assert.Empty(cancionesObtenidas);
    }
}
