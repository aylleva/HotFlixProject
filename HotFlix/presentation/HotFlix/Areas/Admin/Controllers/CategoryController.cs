using HotFlix.Application.Abstraction.Services;
using HotFlix.Application.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace HotFlix.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
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
        public async Task<IActionResult> Create(CreateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDto);
            }

            var result=await _service.AnyAsync(c=>c.Name == categoryDto.Name);
            if (result)
            {
                ModelState.AddModelError(nameof(CreateCategoryDto.Name), "Category is allready exist");
                return View(categoryDto);
            }

            _service.CreateAsync(categoryDto);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int Id)
        {
            var category = await _service.GetbyIdAsync(Id);
            if (category == null) throw new Exception("Category is not found!");

            UpdateCategoryDto categoryDto = new() { Name = category.Name };
            return View(categoryDto);
        }

        [HttpPost]
        public async Task<IActionResult> Update(int Id, UpdateCategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryDto);
            }

            var result=await _service.AnyAsync(c=>c.Name.Trim() == categoryDto.Name.Trim() && c.Id!=Id);
            if (result)
            {
                ModelState.AddModelError(nameof(CreateCategoryDto.Name), "Category is allready exist");
                return View(categoryDto);
            }
            _service.UpdateAsync(Id, categoryDto);
            
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int Id)
        {
            _service.DeleteAsync(Id);
            return RedirectToAction(nameof(Index));
        }
    }
}
