using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Mail { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Clave { get; set; }


    }
}
