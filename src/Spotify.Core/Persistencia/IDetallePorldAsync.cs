using System.Numerics;

namespace Spotify.Core.Persistencia
{
    public interface IDetallePorIdAsync<T, N> where N : IBinaryNumber<N>
    {
        Task<T?> DetalleDe(N id);
    }
}