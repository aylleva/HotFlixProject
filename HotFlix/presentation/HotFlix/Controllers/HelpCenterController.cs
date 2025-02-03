using Microsoft.AspNetCore.Mvc;

namespace HotFlix.Controllers
{
    public class HelpCenterController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
