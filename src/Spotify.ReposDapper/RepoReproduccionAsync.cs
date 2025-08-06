using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using System.Data;

namespace Spotify.ReposDapper;

public class RepoReproduccionAsync : RepoGenerico, IRepoReproduccionAsync
{
    public RepoReproduccionAsync(IDbConnection conexion)
        : base(conexion) { }

    public async Task<uint> Alta(Reproduccion reproduccion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidHistorial", direction: ParameterDirection.Output);
        parametros.Add("@unidUsuario", reproduccion.usuario.idUsuario);
        parametros.Add("@unidCancion", reproduccion.cancion.idCancion);
        parametros.Add("@unFechaReproduccion", reproduccion.FechaReproduccion);
        await _conexion.ExecuteAsync("altaHistorial_reproduccion", parametros, commandType: CommandType.StoredProcedure);

        reproduccion.IdHistorial = parametros.Get<uint>("@unidHistorial");
        return reproduccion.IdHistorial;
    }

    public async Task<Reproduccion?> DetalleDe(uint idHistorial)
    {
        var query = "SELECT * FROM HistorialReproduccion WHERE idHistorial = @idHistorial";
        var reproduccion = await _conexion.QueryFirstOrDefaultAsync<Reproduccion>(query, new { idHistorial });
        return reproduccion;
    }

    public async Task<List<Reproduccion>> Obtener()
    {
        var lista = await EjecutarSPConReturnDeTipoListaAsync<Reproduccion>("ObtenerHistorialReproduccion");
        return lista.ToList();
    }
} 