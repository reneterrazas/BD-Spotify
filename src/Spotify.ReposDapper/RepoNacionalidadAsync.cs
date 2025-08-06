using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.ReposDapper
{
    public class RepoNacionalidadAsync : RepoGenerico, IRepoNacionalidadAsync
    {
        public RepoNacionalidadAsync(IDbConnection conexion)
            : base(conexion) { }

        public async Task<uint> Alta(Nacionalidad nacionalidad)
        {
            var parametros = new DynamicParameters();
            parametros.Add("@unidNacionalidad", direction: ParameterDirection.Output);
            parametros.Add("@unPais", nacionalidad.Pais);

            await _conexion.ExecuteAsync("altaNacionalidad", parametros, commandType: CommandType.StoredProcedure);

            nacionalidad.idNacionalidad = parametros.Get<uint>("@unidNacionalidad");
            return nacionalidad.idNacionalidad;
        }

        public async Task<Nacionalidad?> DetalleDe(uint idNacionalidad)
        {
            var query = "SELECT * FROM Nacionalidad WHERE idNacionalidad = @idNacionalidad";
            var nacionalidad = await _conexion.QueryFirstOrDefaultAsync<Nacionalidad>(query, new { idNacionalidad });
            return nacionalidad;
        }

        public async Task<List<Nacionalidad>> Obtener()
        {
            var lista = await EjecutarSPConReturnDeTipoListaAsync<Nacionalidad>("ObtenerNacionalidades");
            return lista.ToList();
        }
    }
}