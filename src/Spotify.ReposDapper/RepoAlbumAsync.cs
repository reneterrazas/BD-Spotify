namespace Spotify.ReposDapper;

public class RepoAlbumAsync : RepoGenerico, IRepoAlbumAsync
{
    public RepoAlbumAsync(IDbConnection conexion)
        : base(conexion) { }

    public async Task<uint> Alta(Album album)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidAlbum", direction: ParameterDirection.Output);
        parametros.Add("@unTitulo", album.Titulo);
        parametros.Add("@unidArtista", album.artista.idArtista);

        await _conexion.ExecuteAsync("altaAlbum", parametros, commandType: CommandType.StoredProcedure);
        album.idAlbum = parametros.Get<uint>("@unidAlbum");
        return album.idAlbum;
    }

    public async Task<Album?> DetalleDe(uint idAlbum)
    {
        string consultarAlbum = @"SELECT * FROM Album WHERE idAlbum = @idAlbum";

        var Album = await _conexion.QuerySingleOrDefaultAsync<Album>(consultarAlbum, new { idAlbum });

        return Album;
    }

    public async Task Eliminar(uint idAlbum)
    {
        string eliminarCanciones = @"DELETE FROM Cancion WHERE idAlbum = @idAlbum";
        await _conexion.ExecuteAsync(eliminarCanciones, new { idAlbum });

        string eliminarAlbum = @"DELETE FROM Album WHERE idAlbum = @idAlbum";
        await _conexion.ExecuteAsync(eliminarAlbum, new { idAlbum });
    }

    public async Task<List<Album>> Obtener()
    {
        var task = await EjecutarSPConReturnDeTipoListaAsync<Album>("ObtenerAlbum");

        return task.ToList();

    }

    void IEliminar<uint>.Eliminar(uint elemento)
    {
        throw new NotImplementedException();
    }
}
