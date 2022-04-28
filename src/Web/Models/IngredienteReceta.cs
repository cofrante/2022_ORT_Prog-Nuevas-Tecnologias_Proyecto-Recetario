using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class IngredienteReceta
    {
        public int RecetaId { get; set; }
        public Receta Receta { get; set; }
        public int IngredienteId { get; set; }
        public Ingrediente Ingrediente { get; set; }
        [Required]
        public string Cantidad { get; set; }

    }
}
