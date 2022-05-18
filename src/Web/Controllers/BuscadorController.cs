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
            ViewData["Ingredientes"] = new SelectList(_context.Ingredientes, "Id", "Descripcion");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Index(List<int> ingredientes)
        {
            List <Receta> ListaDeRecetas = new List<Receta>();
           
            foreach (var ingrediente in ingredientes)
            {
                List<IngredienteReceta> ingRec = _context.IngredientesRecetas
                    .Include(x => x.Receta)
                    .Where(x => x.IngredienteId == ingrediente).ToList();
                ListaDeRecetas.Add(ingRec.Select(x => x.Receta).FirstOrDefault());
            }
            ViewData["Recetas"] = ListaDeRecetas;
            return View();
        }

    }  
}
