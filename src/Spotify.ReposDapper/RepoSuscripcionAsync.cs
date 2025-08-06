using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using System.Data;

namespace Spotify.ReposDapper;

public class RepoSuscripcionAsync : RepoGenerico, IRepoRegistroAsync
{
    public RepoSuscripcionAsync(IDbConnection conexion)
        : base(conexion) { }

    public async Task<uint> Alta(Registro registro)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidSuscripcion", direction: ParameterDirection.Output);
        parametros.Add("@unidUsuario", registro.usuario.idUsuario);
        parametros.Add("@unidTipoSuscripcion", registro.tipoSuscripcion.IdTipoSuscripcion);
        parametros.Add("@unFechaInicio", registro.FechaInicio);

        await _conexion.ExecuteAsync("altaRegistroSuscripcion", parametros, commandType: CommandType.StoredProcedure);

        registro.idSuscripcion = parametros.Get<uint>("@unidSuscripcion");
        return registro.idSuscripcion;
    }

    public async Task<Registro?> DetalleDe(uint idSuscripcion)
    {
        var query = "SELECT * FROM Suscripcion WHERE idSuscripcion = @idSuscripcion";
        var registro = await _conexion.QueryFirstOrDefaultAsync<Registro>(query, new { idSuscripcion });
        return registro;
    }

    public async Task<List<Registro>> Obtener()
    {
        var lista = await EjecutarSPConReturnDeTipoListaAsync<Registro>("ObtenerSuscripciones");
        return lista.ToList();
    }
} 