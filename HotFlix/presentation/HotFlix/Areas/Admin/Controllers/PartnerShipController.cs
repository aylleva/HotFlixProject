
using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;

namespace HotFlix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PartnerShipController : Controller
    {
        private readonly IPartnerShipService _service;

        public PartnerShipController(IPartnerShipService service)
        {
           _service = service;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePartnerShipDto partnerShipDto)
        {
            if (!ModelState.IsValid)
            {
                return View(partnerShipDto);
            }

            var result = await _service.AnyAsync(c => c.Name == partnerShipDto.Name);
            if (result)
            {
                ModelState.AddModelError(nameof(CreatePartnerShipDto.Name), "PartnerShip is allready exist");
                return View(partnerShipDto);
            }

            await _service.CreateAsync(partnerShipDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int Id)
        {
            var partner = await _service.GetbyIdAsync(Id);
            if (partner == null) throw new Exception("PartnerShip is not found!");

            UpdatePartnerShipDto partnerdto = new() { Name = partner.Name };
            return View(partnerdto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int Id, UpdatePartnerShipDto partnershipdtp)
        {
            if (!ModelState.IsValid)
            {
                return View(partnershipdtp);
            }

            var result = await _service.AnyAsync(c => c.Name.Trim() == partnershipdtp.Name.Trim() && c.Id != Id);
            if (result)
            {
                ModelState.AddModelError(nameof(UpdatePartnerShipDto.Name), "PartnerShip is allready exist");
                return View(partnershipdtp);
            }
           await _service.UpdateAsync(Id, partnershipdtp);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
