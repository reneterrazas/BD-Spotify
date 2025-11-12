using System;
using System.Collections.Generic;
using Spotify.Core;

namespace BD_Sporify._MVC.Models
{
    public class UsuarioViewModel
    {
        public List<Nacionalidad> nacionalidades { get; set; } = new();
        public string NombreUsuario { get; set; }
        public string Email { get; set; } 
        public string Contraseña{ get; set; }
        public uint NacionalidadId { get; set; }
    }
}
