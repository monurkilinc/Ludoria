using Microsoft.AspNetCore.Mvc;

namespace Ludoria.Controllers
{
    public class ReviewController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
