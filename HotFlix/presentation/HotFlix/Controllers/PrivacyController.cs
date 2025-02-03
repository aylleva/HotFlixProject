using Microsoft.AspNetCore.Mvc;

namespace HotFlix.Controllers
{
    public class PrivacyController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
