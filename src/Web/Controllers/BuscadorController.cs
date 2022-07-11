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
            //ViewData["Ingredientes"] = new SelectList(GetIngredientesSelect(), "Id", "DisplayField");
            LoadViewDataIngredientes();
            return View(new List<Receta>());
        }

        private void LoadViewDataIngredientes()
        {
            var datasouce =  from x in _context.Ingredientes.OrderBy(x => x.Descripcion.ToUpper())
                select new
                {
                    x.Id,
                    x.Descripcion,
                    DisplayField = x.Contador == null ? x.Descripcion : String.Format("{0} ( buscado: {1} veces )", x.Descripcion, x.Contador)
                };

            ViewData["Ingredientes"] = new SelectList(datasouce, "Id", "DisplayField");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Index(List<int> ingredientes)
        {
            foreach (var ingredienteId in ingredientes)
            {
                var ingrediente  = _context.Ingredientes.FirstOrDefault(x => x.Id == ingredienteId);
                if (ingrediente.Contador == null)
                {
                    ingrediente.Contador = 1;
                }
                else
                {
                    ingrediente.Contador ++;
                }
                _context.SaveChanges();
            }

            var ingRec = await _context.IngredientesRecetas
                .Include(x => x.Receta)
                .Where(x => ingredientes.Contains(x.IngredienteId)).ToListAsync();
            
            var recetas = new List<Receta>();
            ingRec.ForEach(x => recetas.Add(x.Receta));
            LoadViewDataIngredientes();
            return View(recetas.Distinct());
           
        }

    }  
}
