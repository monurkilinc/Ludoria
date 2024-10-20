using Ludoria.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ludoria.Controllers
{
    public class BlogController : Controller
    {
        private readonly ApplicationDbContext _context;
        public BlogController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var games = _context.Games
                                .Include(g => g.Category)
                                .Include(g => g.Platform)
                                .OrderByDescending(g => g.ReleaseDate)
                                .Take(6)
                                .ToList();
            return View(games);
        }
    }
}
