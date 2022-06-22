using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models.Contextos;

namespace Web.Controllers
{
    public class PruebaController : Controller
    {
        private readonly ContextoBase _context;

        public PruebaController(ContextoBase context)
        {
            _context = context;
        }


    }

    public class Requestasdasd
    {
        public int RecetaId { get; set; }
        public int Puntaje { get; set; }
    }
}
