using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Core.Persistencia
{
    public interface IAltaAsync<T, N>
    {
    Task<N> Alta(T elemento);
    }
}