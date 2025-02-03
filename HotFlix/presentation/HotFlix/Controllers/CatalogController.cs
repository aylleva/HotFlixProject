using Microsoft.AspNetCore.Mvc;

namespace HotFlix.Controllers
{
    public class CatalogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
