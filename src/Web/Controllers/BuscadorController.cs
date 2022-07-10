using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Web.Models;
using Web.Models.Enums;
using Web.Models.Contextos;

namespace Web.Controllers
{
    [Authorize]
    public class BuscadorController : Controller
    {
        private readonly ContextoBase _context;

        public BuscadorController(ContextoBase context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewData["Ingredientes"] = new SelectList(_context.Ingredientes.OrderBy(x => x.Descripcion.ToUpper()), "Id", "Descripcion");
            return View(new List<Receta>());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Index(List<int> ingredientes)
        {
            var ingRec = await _context.IngredientesRecetas
                .Include(x => x.Receta)
                .Where(x => ingredientes.Contains(x.IngredienteId)).ToListAsync();
            
            var recetas = new List<Receta>();
            ingRec.ForEach(x => recetas.Add(x.Receta));
            ViewData["Ingredientes"] = new SelectList(_context.Ingredientes.OrderBy(x => x.Descripcion.ToUpper()), "Id", "Descripcion"); 
            return View(recetas.Distinct());
           
        }

    }  
}
