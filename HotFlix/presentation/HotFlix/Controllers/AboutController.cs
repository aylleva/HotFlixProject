using Microsoft.AspNetCore.Mvc;

namespace HotFlix.Controllers
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
