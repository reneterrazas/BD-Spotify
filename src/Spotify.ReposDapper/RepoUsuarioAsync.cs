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
        parametros.Add("@unEmail", usuario.Email);
        parametros.Add("@unidNacionalidad", usuario.nacionalidad.idNacionalidad);

        await _conexion.ExecuteAsync("altaUsuario", parametros, commandType: CommandType.StoredProcedure);

        usuario.idUsuario = parametros.Get<uint>("@unidUsuario");
        return usuario.idUsuario;
    }

    public async Task<Usuario?> DetalleDe(uint idUsuario)
    {
        string sql = @"
        SELECT *
        FROM Usuario u
        JOIN Nacionalidad Na ON u.idNacionalidad = Na.idNacionalidad 
        WHERE idUsuario = @idUsuario";

        var resultado = await _conexion.QueryAsync<Usuario, Nacionalidad, Usuario>(
            sql,
            (usuario,nacionalidad) =>
            {
                usuario.nacionalidad = nacionalidad;
                return usuario;
            },
            new {idUsuario},
            splitOn: "idNacionalidad"
        );
        return resultado.FirstOrDefault();
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
    public async Task<Usuario?> Login(string email, string contrasenia)
{
    var sql = "SELECT * FROM Usuario WHERE Email = @Email AND Contrasenia = SHA2(@Contrasenia, 256);";
    return await _conexion.QueryFirstOrDefaultAsync<Usuario>(sql, new { Email = email, Contrasenia = contrasenia });
}
} 