namespace Spotify.ReposDapper;
using System.Data;
using System.Threading.Tasks;
using Dapper;
using System.Collections.Generic;
public class RepoGeneroAsync : RepoGenerico, IRepoGeneroAsync
{
    public RepoGeneroAsync(IDbConnection conexion)
        : base(conexion) { }

    public async Task<byte> Alta(Genero genero)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unGenero", genero.genero);
        parametros.Add("@unidGenero", direction: ParameterDirection.Output);

        await _conexion.ExecuteAsync("altaGenero", parametros, commandType: CommandType.StoredProcedure);

        genero.idGenero = parametros.Get<byte>("@unidGenero");
        return genero.idGenero;
    }

    public async Task<Genero?> DetalleDe(byte idGenero)
    {
        var BuscarGeneroPorId = @"SELECT * FROM Genero WHERE idGenero = @idGenero";

        var Buscar = await _conexion.QueryFirstOrDefaultAsync<Genero>(BuscarGeneroPorId, new { idGenero });

        return Buscar;
    }

    public async Task Eliminar(uint idGenero)
    {
        string eliminarHistorialReproducciones = @"
            DELETE FROM HistorialReproducción 
            WHERE idCancion IN (SELECT idCancion FROM Cancion WHERE idGenero = @idGenero)";
        await _conexion.ExecuteAsync(eliminarHistorialReproducciones, new { idGenero });

        string eliminarCanciones = @"DELETE FROM Cancion WHERE idGenero = @idGenero";
        await _conexion.ExecuteAsync(eliminarCanciones, new { idGenero });

        string eliminarGenero = @"DELETE FROM Genero WHERE idGenero = @idGenero";
        await _conexion.ExecuteAsync(eliminarGenero, new { idGenero });
    }

    public async Task<List<Genero>> Obtener()
    {
        var task = await EjecutarSPConReturnDeTipoListaAsync<Genero>("ObtenerGeneros");

        return task.ToList();
    }
}