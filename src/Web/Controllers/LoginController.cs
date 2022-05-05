#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Web.Models;
using Web.Models.Contextos;

namespace Web.Controllers
{
    public class LoginController : Controller
    {
        private readonly ContextoBase _context;

        public LoginController(ContextoBase context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([Bind("Email,Clave")] UsuarioLogin request)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Mail == request.Email && u.Clave == request.Clave);

                if (usuario is null)
                    return View();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
