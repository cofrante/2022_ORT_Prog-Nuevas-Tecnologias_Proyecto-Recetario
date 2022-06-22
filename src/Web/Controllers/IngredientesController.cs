using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

using Web.Models;
using Web.Models.Contextos;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class IngredientesController : Controller
    {
        private readonly ContextoBase _context;

        public IngredientesController(ContextoBase context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Ingredientes.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Create([Bind("Id, Descripcion")] Ingrediente ingrediente)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ingrediente);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(ingrediente);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes.FindAsync(id);

            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Descripcion")] Ingrediente ingrediente)
        {
            if (id != ingrediente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dbIngrediente = await _context.Ingredientes
                        .AsNoTracking()
                        .FirstOrDefaultAsync(u => u.Id == id);

                    _context.Update(ingrediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredienteExists(ingrediente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(ingrediente);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingrediente = await _context.Ingredientes.FindAsync(id);
            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IngredienteExists(int id)
        {
            return _context.Ingredientes.Any(e => e.Id == id);
        }

        private bool RecetaExists(int id)
        {
            return _context.Recetas.Any(e => e.Id == id);
        }

        [HttpPost, ActionName("BorrarIngredienteReceta")]
        public async Task<IActionResult> Index(int recetaId, int ingredienteId)
        {
            if (!RecetaExists(recetaId) || !IngredienteExists(ingredienteId))
                return BadRequest();

            var dbIngredienteReceta = await _context.IngredientesRecetas
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.RecetaId == recetaId && u.IngredienteId == ingredienteId);

            if (dbIngredienteReceta is null)
                return BadRequest();

            _context.IngredientesRecetas.Remove(dbIngredienteReceta);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
