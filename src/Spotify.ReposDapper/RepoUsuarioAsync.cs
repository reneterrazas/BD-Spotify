using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
using System.Data;

namespace Spotify.ReposDapper;

public class RepoUsuarioAsync : RepoGenerico, IRepoUsuarioAsinc
{
    public RepoUsuarioAsync(IDbConnection conexion)
        : base(conexion) { }

    public async Task<uint> Alta(Usuario usuario)
    {
        var parametros = new DynamicParameters();
        parametros.Add("@unidUsuario", direction: ParameterDirection.Output);
        parametros.Add("@unNombreUsuario", usuario.NombreUsuario);
        parametros.Add("@unaContrasenia", usuario.Contrasenia);
        parametros.Add("@unEmail", usuario.Gmail);
        parametros.Add("@unidNacionalidad", usuario.nacionalidad.idNacionalidad);

        await _conexion.ExecuteAsync("altaUsuario", parametros, commandType: CommandType.StoredProcedure);

        usuario.idUsuario = parametros.Get<uint>("@unidUsuario");
        return usuario.idUsuario;
    }

    public async Task<Usuario?> DetalleDe(uint idUsuario)
    {
        string query = "SELECT * FROM Usuario WHERE idUsuario = @idUsuario";
        var usuario = await _conexion.QueryFirstOrDefaultAsync<Usuario>(query, new { idUsuario });
        return usuario;
    }

    public async Task<List<Usuario>> Obtener()
    {
        var lista = await EjecutarSPConReturnDeTipoListaAsync<Usuario>("ObtenerUsuarios");
        return lista.ToList();
    }

    public void Eliminar(uint elemento)
    {
        throw new NotImplementedException();
    }
} 