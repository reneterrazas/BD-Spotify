namespace Spotify.Core.Persistencia;

public interface IEliminar<T>
{
    void Eliminar(T elemento);
    Task EliminarAsync(T elemento);
}
