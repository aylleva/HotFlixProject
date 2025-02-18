using Microsoft.AspNetCore.Mvc;

namespace HotFlix.Areas.Admin.Controllers
{
    public class SettingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
