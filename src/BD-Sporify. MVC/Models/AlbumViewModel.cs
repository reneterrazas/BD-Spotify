using System;
using System.Collections.Generic;
using Spotify.Core;

namespace BD_Sporify._MVC.Models
{
    public class AlbumViewModel
    {
        public List<Artista> artistas { get; set; } = new();
        public string Titulo { get; set; }
        public DateTime FechaLanzamiento { get; set; } = DateTime.Now;
        public uint ArtistaId { get; set; }
    }
}
