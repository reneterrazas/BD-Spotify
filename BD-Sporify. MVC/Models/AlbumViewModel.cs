using System.Collections.Generic;
using Spotify.Core;

namespace BD_Sporify._MVC.Models
{
   namespace SpotifyMVC.Models
{
    public class AlbumViewModel
    {
        public Album album { get; set; }
        public List<Artista> artistas { get; set; } = new();
        public string Titulo { get; set; }
        public DateTime FechaLanzamiento { get; set; } = DateTime.Now;
        public uint ArtistaId { get; set; }

    }
}
}
