using Ludoria.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ludoria.Controllers
{
    public class GameController:Controller
    {
        private readonly ApplicationDbContext _context;

        public GameController (ApplicationDbContext context)
        {
            _context = context;
        }

        //Oyun Inceleme Blog sayfasi icin oyunlari listeledim.
        public IActionResult Index()
        {
            var games=_context.Games.Include(x=>x.Platform)
                                    .Include(y=>y.Category)
                                    .ToList();

            return View(games);
        }


    }
}
