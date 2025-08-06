using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using System.Data;

namespace Spotify.ReposDapper;

public class RepoPlaylistAsync : RepoGenerico, IRepoPlaylistAsync
{
    public RepoPlaylistAsync(IDbConnection conexion)
        : base(conexion) { }

    public async Task<uint> Alta(PlayList playlist)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidPlaylist", direction: ParameterDirection.Output);
        parametros.Add("@unNombre", playlist.Nombre);
        parametros.Add("@unidUsuario", playlist.usuario.idUsuario);

        await _conexion.ExecuteAsync("altaPlaylist", parametros, commandType: CommandType.StoredProcedure);

        playlist.idPlaylist = parametros.Get<uint>("@unidPlaylist");
        return playlist.idPlaylist;
    }

    public async Task<PlayList?> DetalleDe(uint idPlaylist)
    {
        var query = "SELECT * FROM Playlist WHERE idPlaylist = @idPlaylist";
        var playlist = await _conexion.QueryFirstOrDefaultAsync<PlayList>(query, new { idPlaylist });
        return playlist;
    }

    public async Task<List<PlayList>> Obtener()
    {
        var lista = await EjecutarSPConReturnDeTipoListaAsync<PlayList>("ObtenerPlayLists");
        return lista.ToList();
    }
} 