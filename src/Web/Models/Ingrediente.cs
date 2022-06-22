using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class Ingrediente
    {
        public int Id { get; set; }
        [Required]
        public string Descripcion { get; set; }

        public List<IngredienteReceta> Recetas { get; set; } = new List<IngredienteReceta>();
    }
}