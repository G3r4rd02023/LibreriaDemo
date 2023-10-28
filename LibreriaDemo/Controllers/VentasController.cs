using LibreriaDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibreriaDemo.Controllers
{
    public class VentasController : Controller
    {
        private readonly LibreriaContext _context;

        public VentasController(LibreriaContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Lista()
        {
            return View(await _context.Ventas
                .Include(v=>v.Libro).ThenInclude(l=>l.Autor)
                .Include(v=>v.Libro).ThenInclude(l=>l.Categoria)
                .ToListAsync());
        }
    }
}
