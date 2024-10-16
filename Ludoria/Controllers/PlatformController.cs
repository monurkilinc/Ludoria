using Microsoft.AspNetCore.Mvc;

namespace Ludoria.Controllers
{
    public class PlatformController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
