using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.ViewModels;
using HotFlix.Domain;
using HotFlix.Domain.Models;
using HotFlix.Infrastructure.Implementations.Services;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using Stripe;
using System.Security.Claims;

namespace HotFlix.Controllers
{
    public class PricingController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _usermeneger;
        private readonly ViewRenderer _renderer;
        private readonly IEmailService emailservice;

        public PricingController(AppDbContext context
            , UserManager<AppUser> usermeneger
            , ViewRenderer renderer
            , IEmailService emailservice)
        {
            _context = context;
            _usermeneger = usermeneger;
            _renderer = renderer;
            this.emailservice = emailservice;
        }
        public async Task<IActionResult> Index()
        {
            PricingVM pricingVM = new PricingVM
            {
                Plans = await _context.PremiumPlans.ToListAsync()
            };

            return View(pricingVM);
        }

        [HttpPost]
        public async Task<IActionResult> Index(PricingVM ordervm, string stripeEmail, string stripeToken)
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

            ViewBag.Total = total;
            user.PremiumPlanId = ordervm.PlanId;
            user.IsPremium = true;

            order.Status = true;
            await _context.PlanOrders.AddAsync(order);
            await _context.SaveChangesAsync();

            string body = @"<!DOCTYPE html>
<html lang=""az"">
<head>
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Premium Plan Confirmation</title>
    <style>
        body { font-family: Arial, sans-serif; background-color: #f4f4f4; margin: 0; padding: 0; }
        .email-container { max-width: 600px; margin: 20px auto; background: white; padding: 20px; border-radius: 8px; box-shadow: 0 0 10px rgba(0, 0, 0, 0.1); }
        .header { text-align: center; background: #007bff; color: white; padding: 15px; border-radius: 8px 8px 0 0; }
        .content { padding: 20px; text-align: center; }
        .content h2 { color: #333; }
        .content p { font-size: 16px; color: #666; }
        .plan-details { background: #f9f9f9; padding: 15px; border-radius: 5px; margin-top: 20px; }
        .plan-details p { margin: 5px 0; font-weight: bold; }
        .button { display: inline-block; margin-top: 20px; padding: 10px 20px; background: #007bff; color: white; text-decoration: none; font-size: 16px; border-radius: 5px; }
        .button:hover { background: #0056b3; }
        .footer { margin-top: 20px; font-size: 14px; color: #888; text-align: center; }
    </style>
</head>";
            body += @$"<body>

    <div class=""""email-container"""">
        <div class=""""header"""">
            <h1>Premium Plan Is Active Now!</h1>
        </div>
        <div class=""""content"""">
            <h2>Hello Dear,{order.User.Name}</h2>
            <p>You have successfully purchased your **Premium Plan**. 🎉</p>
            
            <div class=""""plan-details"""">
                <p>📌 **Plan Name:** {order.PremiumPlanPlan.PlanName}</p>
                <p>💰 **Price:** {order.Price} AZN</p>
                <p>⏳ **Expires:** {order.PremiumPlanPlan.Expires}</p>
            </div>
        </div>
        <div class=""""footer"""">
            <p>Contact With Us <a href=""""HotFlixContact@gmail.com"""">HotFlixContact@gmail.com</a></p>
            <p>&copy; 2025 Your Company. All rights reserved.</p>
        </div>
    </div>

</body>
</html>"";
";
            await emailservice.SendEmailAsync(user.Email, "Premium Plan Info", body, true);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }


}
