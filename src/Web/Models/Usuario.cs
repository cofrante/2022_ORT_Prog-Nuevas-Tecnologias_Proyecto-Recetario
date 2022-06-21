using System.ComponentModel.DataAnnotations;
using Web.Models.Enums;

namespace Web.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        
        [Required]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        
        [Required]
        [DataType(DataType.Password)]
        public string Clave { get; set; }

        [Required]
        public PerfilesUsuario Perfil { get; set; } = PerfilesUsuario.Usuario;

        [Required]
        [DisplayAttribute(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }
    }
}
