namespace Spotify.Core.Persistencia;

public interface IListado<T>
{
    IList<T> Obtener();
    Task<IList<T>> ObtenerAsync();
}