using Microsoft.AspNetCore.Mvc.Rendering;

namespace BD_Sporify._MVC.Models

{
    
public class RegisterViewModel
    {
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Contrasenia { get; set; } = string.Empty;
        public string ConfirmarContrasenia { get; set; } = string.Empty;

        // Para la nacionalidad
        public int NacionalidadId { get; set; }

        // Usamos SelectList en vez de List<SelectListItem>
        public SelectList Nacionalidades { get; set; } = new SelectList();
    }
}
