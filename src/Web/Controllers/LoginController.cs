#nullable disable
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

using Web.Models;
using Web.Models.Contextos;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Models.Enums;

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

        [HttpGet]
        public async Task<IActionResult> CreateUser()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> NoAutorizado()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Login");
        }

        [HttpPost("{controller}")]
        public async Task<IActionResult> Login([Bind("Email,Clave")] UsuarioLogin request)
        {
            if (ModelState.IsValid)
            {
                var usuario = await _context.Usuarios
                    .FirstOrDefaultAsync(u => u.Mail == request.Email && u.Clave == request.Clave);

                if (usuario is null)
                    return View("Index", request);

                await SignIn(usuario);

                return RedirectToAction("Index", "Home");
            }

            return View("Index", request);
        }

        private async Task SignIn(Usuario usuario)
        {
            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            identity.AddClaim(new Claim(ClaimTypes.Name, usuario.Name));
            identity.AddClaim(new Claim(ClaimTypes.Role, usuario.Perfil.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.Email, usuario.Mail));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);
        }
    }
}
