using Microsoft.AspNetCore.Mvc;

namespace HotFlix.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
