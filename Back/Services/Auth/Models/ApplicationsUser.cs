using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace s10.Back.Services.Auth.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Key]
        public int IdCliente { get; private set; }

        public string? Foto { get; set; }
        public string? Nombres { get; set; }
        public string? Apellidos { get; set; }

        public string? Direccion { get; set; } = String.Empty;
        public string? Provincia { get; set; }
        public string? Pais { get; set; }
    }
}
