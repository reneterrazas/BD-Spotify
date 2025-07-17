using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Core.Persistencia
{
    public interface IMatcheoAsync
    {
    Task<List<string>?> Matcheo(string Cadena);
    }
}