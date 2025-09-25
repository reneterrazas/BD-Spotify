using Spotify.Core;
namespace BD_Sporify._MVC.Models;


    public class ArtistaViewModel
    {
        public  Album album { get; set; }
        public  List<Artista> artistas { get; set; }
        public  Artista artista { get; set; }
    }