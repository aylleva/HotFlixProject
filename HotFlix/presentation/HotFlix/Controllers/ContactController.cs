using Microsoft.AspNetCore.Mvc;

namespace HotFlix.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
