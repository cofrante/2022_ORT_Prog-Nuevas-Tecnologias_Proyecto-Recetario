using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class UsuarioLogin
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Clave { get; set; }
    }
}
