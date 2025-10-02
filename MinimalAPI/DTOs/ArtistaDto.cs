using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalAPI.DTOs
{
    public record struct ArtistaDto(
        uint IdArtista,
        string NombreArtistico
    );


    public record struct CrearArtistaDto(
        string NombreArtistico,
        string Nombre,
        string Apellido
    );
}