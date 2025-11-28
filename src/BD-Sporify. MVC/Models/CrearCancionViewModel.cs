using System;
using System.Collections.Generic;
using Spotify.Core;

namespace BD_Sporify._MVC.Models
{
    public class CrearCancionViewModel
    {
        public List<Album>? albums { get; set; } = new();

        public List<Genero>? generos { get; set; } = new();
        public string Titulo { get; set; } = String.Empty;
        public TimeSpan duration { get; set; }
        public uint AlbumId { get; set; }
        public byte GeneroId { get; set; } 

    }
}
