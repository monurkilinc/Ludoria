using Microsoft.AspNetCore.Mvc;

namespace Ludoria.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
