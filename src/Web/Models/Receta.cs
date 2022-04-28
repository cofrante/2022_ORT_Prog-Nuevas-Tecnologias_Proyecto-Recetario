using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Receta
    {
        public int Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string Descripcion { get; set; }
        public List<IngredienteReceta> Ingredientes { get; set; }

    }
}
