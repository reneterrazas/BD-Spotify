namespace Spotify.Core.Persistencia;
public interface IRepoUsuarioAsinc : IAltaAsync<Usuario, uint>, IListadoAsync<Usuario>, IEliminar<uint>, IDetallePorIdAsync<Usuario, uint>
{
Task<Usuario?> Login(string email, string contrasenia); 
}