using HotFlix.Application.ViewModels;
using HotFlix.Domain.Models;
using HotFlix.Persistence.DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotFlix.Controllers
{
    public class ContactController : Controller
    {
        private readonly UserManager<AppUser> _usermeneger;
        private readonly AppDbContext _context;

        public ContactController(UserManager<AppUser> usermeneger,AppDbContext context)
        {
           _usermeneger = usermeneger;
           _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(ContactVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            var email=await _usermeneger.Users.FirstOrDefaultAsync(e=>e.Email==vm.Email);
            if(email is null)
            {
                ModelState.AddModelError(string.Empty, "Email does not exists!Make sure that you login!");
                return View(vm);
            }

            Contact contact = new Contact() { 
            Name = vm.Name,
            Email = vm.Email,
            PartnerShip=vm.PartnerShip,
            Message = vm.Message,
            };

            await _context.Contacts.AddAsync(contact);
            await _context.SaveChangesAsync();
            TempData["SuccesMessage"] = $"<div class=\"text-center p-2 text-black bg-warning\">Your Message was sent Succesfully! We will check your Mail as soon as possible!Thank You!</div>";

            return RedirectToAction(nameof(ContactController.Index),"Contact");
        }

    }
}
