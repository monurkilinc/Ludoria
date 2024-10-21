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
        //Oyun listesini Index sayfasında gösterir.
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
            await PopulateViewBags();
            return View("AddGame");
        }

        // Oyun ekleme için POST action
        [HttpPost]
        public async Task<IActionResult> Create(Game game, IFormFile image)
        {
            // ModelState geçerliyse işlem yapılır, değilse hata mesajı döndürülür.
            if (ModelState.IsValid)
            {
                // Resim varsa kaydedilir
                if (image != null && image.Length > 0)
                {
                    game.ImageURL = await SaveImage(image);
                }

                // Resim yoksa kullanıcıya hata dondurulur
                else
                {
                    ModelState.AddModelError("Image", "Resim yüklemek zorunludur.");
                    await PopulateViewBags();
                    return View("AddGame", game);
                }

                // Yeni eklenen oyun DbContext'e eklenir
                _context.Add(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Hata varsa ViewBag yeniden doldurulur ve girilen degerler ekranda gosterilir.
            await PopulateViewBags();
            return View("AddGame", game);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            // ID null ise hata döndürülür
            if (id == null)
            {
                return NotFound();
            }

            // Veritabanından oyunlar çekilir
            var game = await _context.Games
                .Include(x => x.Platform)
                .Include(y => y.Category)
                .FirstOrDefaultAsync(z => z.Id == id);

            // Oyun yoksa hata döndürülür
            if (game == null)
            {
                return NotFound();
            }

            // Kategoriler ve platformlar ViewBag'e eklenir
            await PopulateViewBags();
            return View("EditGame", game);
        }

        // Oyun guncelleme için POST action
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Game game, IFormFile image)
        {
            //ID'ler uyuşmuyorsa hata döndürülür
            if (id != game.Id)
            {
                return NotFound();
            }

            // ModelState geçerliyse işlem yapılır
            if (ModelState.IsValid)
            {
                // Resim varsa kaydedilir
                if (image != null && image.Length > 0)
                {
                    game.ImageURL = await SaveImage(image);
                }

                // Oyun güncellenir ve değişiklikler kaydedilir
                _context.Update(game);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            // Hata varsa ViewBag yeniden doldurulur ve form tekrar gösterilir
            await PopulateViewBags();
            return View("EditGame", game);
        }

        // Oyun silme için POST action
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

        // Oyunun var olup olmadığını kontrol eden metod
        private bool GameExists(int id)
        {
            return _context.Games.Any(e => e.Id == id);
        }

        // ViewBag ile kategorileri ve platformları dolduran metod
        private async Task PopulateViewBags()
        {
            ViewBag.Categories = await _context.Categories.ToListAsync();
            ViewBag.Platforms = await _context.Platforms.ToListAsync();
        }

        // Yüklenen resim dosyasını kaydeden metod
        private async Task<string> SaveImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return null;

            var fileName = Path.GetRandomFileName() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_environment.WebRootPath, "admin", "assets", "images", "game", fileName);

            // Resim dosyası belirtilen yola kaydedilir
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }
            return $"/admin/assets/images/game/{fileName}";
        }
    }
}
