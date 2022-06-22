#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

using Web.Models;
using Web.Models.Contextos;

namespace Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class RecetasController : Controller
    {
        private readonly ContextoBase _context;

        public RecetasController(ContextoBase context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Recetas.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receta = await _context.Recetas
                .Include(x => x.Ingredientes)
                    .ThenInclude(x => x.Ingrediente)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (receta == null)
            {
                return NotFound();
            }

            return View(receta);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombre,Descripcion,FechaAlta,FechaEdicion,Puntaje")] Receta receta)
        {
            receta.Usuario = User.FindFirstValue(ClaimTypes.Name);
            
            if (ModelState.IsValid)
            {
                receta.FechaAlta = DateTime.Now;
                _context.Add(receta);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(receta);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receta = await _context.Recetas
                .Include(x => x.Ingredientes)
                    .ThenInclude(x => x.Ingrediente)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (receta == null)
            {
                return NotFound();
            }

            ViewData["Ingredientes"] = new SelectList(_context.Ingredientes, "Id", "Descripcion");
            return View(receta);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombre,Descripcion,Usuario,FechaAlta,FechaEdicion,Puntaje")] Receta receta)
        {
            if (id != receta.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var dbReceta = await _context.Recetas
                    .AsNoTracking()
                    .FirstOrDefaultAsync(u => u.Id == id);

                    receta.FechaAlta = dbReceta.FechaAlta;
                    receta.Puntaje = dbReceta.Puntaje;
                    receta.FechaEdicion = DateTime.Now;
                    receta.Usuario = dbReceta.Usuario; 

                    _context.Update(receta);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RecetaExists(receta.Id))
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
            return View(receta);
        }
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var receta = await _context.Recetas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (receta == null)
            {
                return NotFound();
            }

            return View(receta);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var receta = await _context.Recetas.FindAsync(id);
            _context.Recetas.Remove(receta);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecetaExists(int id)
        {
            return _context.Recetas.Any(e => e.Id == id);
        }
        private bool IngredienteExists(int id)
        {
            return _context.Ingredientes.Any(e => e.Id == id);
        }

        [HttpPost, ActionName("Puntaje")]
        public async Task<IActionResult> Index(int recetaId, int puntaje)
        {
            var dbReceta = await _context.Recetas
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == recetaId);

            if (dbReceta is null)
                return BadRequest();

            dbReceta.Puntaje = puntaje;

            _context.Update(dbReceta);
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpPost, ActionName("AgregarIngredienteReceta")]
        public async Task<IActionResult> Index(int recetaId, int ingredienteId, string cantidad)
        {
            if (!RecetaExists(recetaId) || !IngredienteExists(ingredienteId))
                return BadRequest();

            var dbIngredienteReceta = await _context.IngredientesRecetas
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.RecetaId == recetaId && u.IngredienteId == ingredienteId);

            if (dbIngredienteReceta is null)
            {
                _context.IngredientesRecetas.Add(new IngredienteReceta() { Cantidad = cantidad, IngredienteId = ingredienteId, RecetaId = recetaId });
            }
            else
            {
                dbIngredienteReceta.Cantidad = cantidad;
                _context.IngredientesRecetas.Update(dbIngredienteReceta);
            }

            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
