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

        public List<IngredienteReceta> Ingredientes { get; set; }= new List<IngredienteReceta>();

        public string Usuario { get; set; } = string.Empty;

        [Required]
        [DisplayAttribute(Name = "Fecha de Creación")]
        public DateTime FechaAlta { get; set; }

        [Required]
        [DisplayAttribute(Name = "Fecha de Edición")]
        public DateTime FechaEdicion { get; set; } = DateTime.MinValue;

        [Range(0,5)]
        public int Puntaje { get; set; } = 0;

        [Range(0, 5)]
        [DisplayAttribute(Name = "Promedio")]
        public Decimal? PuntajePromedio { get; set; } = 0;

    }
}
