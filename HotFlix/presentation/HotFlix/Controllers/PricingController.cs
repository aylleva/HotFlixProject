using HotFlix.Application.ViewModels;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotFlix.Controllers
{
    public class PricingController : Controller
    {
        private readonly AppDbContext _context;

        public PricingController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            PricingVM pricingVM = new PricingVM { 
            Plans=await _context.PremiumPlans.ToListAsync()
            };

            return View(pricingVM);
        }
    }
}
