using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Core.Persistencia
{
    public interface IListadoAsync<T>
    {
        Task<List<T>> Obtener();
    }
}