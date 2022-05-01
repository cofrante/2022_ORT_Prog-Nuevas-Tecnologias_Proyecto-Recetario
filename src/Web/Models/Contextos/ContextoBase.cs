using Microsoft.EntityFrameworkCore;

namespace Web.Models.Contextos
{
    public class ContextoBase : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Receta> Recetas { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }
        public DbSet<IngredienteReceta> IngredientesRecetas { get; set; }

        public ContextoBase(DbContextOptions<ContextoBase> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IngredienteReceta>()
            .HasKey(j => new { j.IngredienteId, j.RecetaId });

            modelBuilder.Entity<IngredienteReceta>()
            .HasOne(e => e.Ingrediente)
            .WithMany(r => r.Recetas)
            .HasForeignKey(o => o.IngredienteId);

            modelBuilder.Entity<IngredienteReceta>()
            .HasOne(e => e.Receta)
            .WithMany(r => r.Ingredientes)
            .HasForeignKey(o => o.RecetaId);
        }

    }
}
