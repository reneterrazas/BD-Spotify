using System.Diagnostics.CodeAnalysis;
using System.Reflection.Metadata.Ecma335;

namespace Spotify.ReposDapper;

public class RepoTipoSuscripcionAsync : RepoGenerico, IRepoTipoSuscripcionAsync
{
    public RepoTipoSuscripcionAsync(IDbConnection conexion) 
        : base(conexion) {}

    public async Task<uint> Alta(TipoSuscripcion tipoSuscripcion)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidTipoSuscripcion", direction: ParameterDirection.Output);
        parametros.Add("@unCosto", tipoSuscripcion.Costo);
        parametros.Add("@unaDuracion", tipoSuscripcion.Duracion);
        parametros.Add("@UntipoSuscripcion", tipoSuscripcion.Tipo);

        await _conexion.ExecuteAsync("altaTipoSuscripcion", parametros, commandType: CommandType.StoredProcedure);

        tipoSuscripcion.IdTipoSuscripcion = parametros.Get<uint>("@unidTipoSuscripcion");

        return tipoSuscripcion.IdTipoSuscripcion;
    }

    public Task<TipoSuscripcion> DetalleDe(uint idTipoSuscripcion)
    {
        var BuscarTipoSuscripcionPorId = @"
        Select * 
        FROM TipoSuscripcion
        Where idTipoSuscripcion = @idTipoSuscripcion
        ";
        
        var TipoSuscripcion = _conexion.QueryFirstOrDefaultAsync<TipoSuscripcion>(BuscarTipoSuscripcionPorId, new {idTipoSuscripcion});

        return TipoSuscripcion;
    }

    public async Task<List<TipoSuscripcion>> Obtener() {
        var tipoSuscripciones = await EjecutarSPConReturnDeTipoListaAsync<TipoSuscripcion>("ObtenerTipoSuscripciones");
        return tipoSuscripciones.ToList();
    }
}