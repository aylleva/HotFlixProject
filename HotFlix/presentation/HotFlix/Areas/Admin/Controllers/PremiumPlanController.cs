using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotFlix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class PremiumPlanController : Controller
    {
        private readonly IPremiumService _service;

        public PremiumPlanController(IPremiumService service)
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
        public async Task<IActionResult> Create(CreatePremiumPlanDto plandto)
        {
            if (!ModelState.IsValid)
            {
                return View(plandto);
            }

            var result = await _service.AnyAsync(c => c.PlanName == plandto.PlanName);
            if (result)
            {
                ModelState.AddModelError(nameof(CreatePremiumPlanDto.PlanName), "Plan is allready exist");
                return View(plandto);
            }

            await _service.CreateAsync(plandto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int Id)
        {
            var plan = await _service.GetbyIdAsync(Id);
            if (plan == null) throw new Exception("Plan is not found!");

            UpdatePremiumPlanDto planDto = new() { 
            PlanName= plan.PlanName,
            Expires= plan.Expires,
            Quality= plan.Quality,
            Price= plan.Price
            };
            return View(planDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int Id, UpdatePremiumPlanDto planDto)
        {
            if (!ModelState.IsValid)
            {
                return View(planDto);
            }

            var result = await _service.AnyAsync(c => c.PlanName.Trim() == planDto.PlanName.Trim() && c.Id != Id);
            if (result)
            {
                ModelState.AddModelError(nameof(UpdatePremiumPlanDto.PlanName), "Plan is allready exist");
                return View(planDto);
            }
            await _service.UpdateAsync(Id, planDto);

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int Id)
        {
            await _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
