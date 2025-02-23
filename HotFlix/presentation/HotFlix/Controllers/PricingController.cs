using HotFlix.Application.ViewModels;
using HotFlix.Domain;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stripe;
using System.Security.Claims;

namespace HotFlix.Controllers
{
    public class PricingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _usermeneger;

        public PricingController(AppDbContext context,UserManager<AppUser> usermeneger)
        {
            _context = context;
           _usermeneger = usermeneger;
        }
        public async Task<IActionResult> Index()
        {
            PricingVM pricingVM = new PricingVM { 
            Plans=await _context.PremiumPlans.ToListAsync()
            };

            return View(pricingVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PricingVM ordervm,string stripeEmail,string stripeToken)
        {
            var user = await _usermeneger.Users.FirstOrDefaultAsync(u => u.Id == User.FindFirstValue(ClaimTypes.NameIdentifier));

            if (!ModelState.IsValid)
            {
                return View(ordervm);
            }

            decimal total = _context.PremiumPlans.FirstOrDefault(p => p.Id == ordervm.PlanId).Price;

            PlanOrder order = new PlanOrder()
            {
                Name = ordervm.Name,
                Surname = ordervm.Surname,
                PremiumPlanId = ordervm.PlanId,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Price = total,
                
            };

            var optionCust = new CustomerCreateOptions
            {
                Email = stripeEmail,
                Name = user.Name + " " + user.Surname,
            };
            var serviceCust = new CustomerService();
            Customer customer = serviceCust.Create(optionCust);

            total = total * 100;
            var optionsCharge = new ChargeCreateOptions
            {

                Amount = (long)total,
                Currency = "USD",
                Description = "Premium Plan Activated",
                Source = stripeToken,
                ReceiptEmail = stripeEmail,
               


            };
            var serviceCharge = new ChargeService();
            Charge charge = serviceCharge.Create(optionsCharge);
            if (charge.Status != "succeeded")
            {
                ModelState.AddModelError(string.Empty, "Something is gone wrong with the payment! Try Again!!");
                return View();
            }
            
            ViewBag.Total=total;
            user.PremiumPlanId=ordervm.PlanId;
            user.IsPremium = true;

            order.Status = true;
            await _context.PlanOrders.AddAsync(order);

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }

    
}
