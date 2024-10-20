
using Ludoria.Context;
using Ludoria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ludoria.Controllers
{
    public class GameController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public GameController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task<IActionResult> Index()
        {
            var games = await _context.Games
                .Include(x => x.Platform)
                .Include(y => y.Category)
                .ToListAsync();

            return View(games);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Platforms = await _context.Platforms.ToListAsync();
            return View("AddGame");
        }

        [HttpPost]
        public async Task<IActionResult> Create(Game game, IFormFile image)
        {
            if (image == null || image.Length == 0)
            {
                ModelState.AddModelError("Image", "Resim yüklemek zorunludur.");
            }

            if (ModelState.IsValid)
            {
                if (image != null && image.Length > 0)
                {
                    game.ImageURL = await SaveImage(image);
                    Console.WriteLine($"Image saved. URL: {game.ImageURL}");
                }
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View("AddGame", game);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var game = await _context.Games
                .Include(x => x.Platform)
                .Include(y => y.Category)
                .FirstOrDefaultAsync(z => z.Id == id);

            if (game == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Platforms = await _context.Platforms.ToListAsync();
            return View("EditGame", game);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Game game, IFormFile image)
        {
            if (id != game.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null && image.Length > 0)
                    {
                        game.ImageURL = await SaveImage(image);
                        Console.WriteLine($"Image updated. URL: {game.ImageURL}");
                    }
                    _context.Update(game);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameExists(game.Id))
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
            return View("EditGame", game);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var game = await _context.Games.FindAsync(id);
            if (game != null)
            {
                _context.Games.Remove(game);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }

        

        private async Task<string> SaveImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var fileName = Path.GetFileName(file.FileName);
            var directoryPath = Path.Combine(_environment.WebRootPath, "admin", "assets", "images", "game", fileName);
            var filePath = Path.Combine(directoryPath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var relativeUrl = $"/admin/assets/images/game/{fileName}";
            return relativeUrl;
        }


    }
}



//using Ludoria.Context;
//using Ludoria.Models;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore.Metadata.Internal;

//namespace Ludoria.Controllers
//{
//    public class GameController : Controller
//    {
//        private readonly ApplicationDbContext _context;

//        public GameController(ApplicationDbContext context)
//        {
//            _context = context;
//        }

//        //Oyun Inceleme Blog sayfasi icin oyunlari listeledim.
//        public IActionResult Index()
//        {
//            ViewBag.games = _context.Games.Include(x => x.Platform)
//                                    .Include(y => y.Category)
//                                    .ToList();

//            return View();
//        }
//        public IActionResult Create()
//        {
//            ViewBag.Categories = _context.Categories.ToList();
//            ViewBag.Platforms = _context.Platforms.ToList();
//            return View("AddGame");
//        }
//        [HttpPost]
//        public async Task<IActionResult> Create(Game game, IFormFile image)
//        {
//            if (ModelState.IsValid)
//            {
//                if (image != null && image.Length > 0)
//                {
//                    var fileName = Path.GetFileName(image.FileName);
//                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "assets", "images", "game", fileName);
//                    using (var stream = new FileStream(filePath, FileMode.Create))
//                    {
//                        await image.CopyToAsync(stream);
//                    }

//                    game.ImageURL = "/admin/assets/images/game/" + fileName;

//                }

//                _context.Games.Add(game);
//                await _context.SaveChangesAsync();
//                return RedirectToAction(nameof(Index));
//            }

//            ViewBag.Categories = await _context.Categories.ToListAsync();
//            ViewBag.Platforms = await _context.Platforms.ToListAsync();
//            return View(game);
//        }
//        public IActionResult Edit(int? id)
//        {
//            if (id is null)
//            {
//                return NotFound();
//            }

//            var game = _context.Games.Include(x => x.Platform)
//                                       .Include(y => y.Category)
//                                       .FirstOrDefault(z => z.Id == id);
//            if (game is null)
//            {
//                return NotFound();
//            }
//            ViewBag.Categories = _context.Categories.ToList();
//            ViewBag.Platforms = _context.Platforms.ToList();
//            return View("EditGame", game);
//        }
//        [HttpPost]
//        public async Task<IActionResult> Edit(int id, Game game, IFormFile image)
//        {
//            if (id != game.Id)
//            {
//                return NotFound();
//            }

//            if (ModelState.IsValid)
//            {
//                try
//                {
//                    if (image != null && image.Length > 0)
//                    {
//                        var fileName = Path.GetFileName(image.FileName);
//                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "admin", "assets", "images", "game", fileName);
//                        using (var stream = new FileStream(filePath, FileMode.Create))
//                        {
//                            await image.CopyToAsync(stream);
//                        }

//                        game.ImageURL = "/admin/assets/images/game/" + fileName;

//                    }

//                    _context.Update(game);
//                    await _context.SaveChangesAsync();
//                }
//                catch (DbUpdateConcurrencyException)
//                {
//                    if (!GameExists(game.Id))
//                    {
//                        return NotFound();
//                    }
//                    else
//                    {
//                        throw;
//                    }
//                }
//                return RedirectToAction(nameof(Index));
//            }

//            ViewBag.Categories = await _context.Categories.ToListAsync();
//            ViewBag.Platforms = await _context.Platforms.ToListAsync();
//            return View(game);
//        }

//        [HttpPost]
//        public IActionResult Delete(int id)
//        {
//            var game = _context.Games.Find(id);
//            if (game != null)
//            {
//                _context.Games.Remove(game);
//                _context.SaveChanges();
//                return RedirectToAction(nameof(Index));
//            }
//            return RedirectToAction("Index", "Game");
//        }
//        [HttpPost]
//        public IActionResult DeleteConfirmed(int id)
//        {
//            var game = _context.Games.Find(id);
//            if (game != null)
//            {
//                _context.Games.Remove(game);
//                _context.SaveChanges();
//                return RedirectToAction("Index", "Game");
//            }
//            return RedirectToAction("Index", "Game");
//        }

//        private bool GameExists(int id)
//        {
//            return _context.Games.Any(e => e.Id == id);
//        }
//    }
//}
