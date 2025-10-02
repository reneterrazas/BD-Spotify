using System.ComponentModel.DataAnnotations;
namespace Spotify.Core
{
    public class Artista
    {
        [Required]
        public uint idArtista { get; set; }
        [Required]
        public required string NombreArtistico { get; set; }
        [Required]
        public required string Nombre { get; set; }
        [Required]
        public required string Apellido { get; set; }
    }
}