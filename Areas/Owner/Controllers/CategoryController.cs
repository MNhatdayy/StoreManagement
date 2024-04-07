using Microsoft.AspNetCore.Mvc;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Service;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class CategoryController : Controller
    {
        public readonly IFoodCategoryService _foodCategoryService;

        public CategoryController(IFoodCategoryService foodCategoryService)
        {
            this._foodCategoryService = foodCategoryService;
        }
        public async Task<ActionResult> Index()
        {
            var foodCategory = await _foodCategoryService.GetAll();
            return View(foodCategory);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FoodCategoryDTO foodCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _foodCategoryService.Create(foodCategoryDTO);
                return RedirectToAction("Index");
            }
            return View(foodCategoryDTO);
        }
        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var foodCategory = await _foodCategoryService.GetById(id);
            if (foodCategory == null)
            {
                return NotFound();
            }
            return View(foodCategory);
        }

        [HttpPost]
        public async Task<ActionResult> Update(int id, FoodCategoryDTO foodCategoryDTO)
        {
            if (id != foodCategoryDTO.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _foodCategoryService.Edit(id, foodCategoryDTO);
                if (result == null)
                {
                    return NotFound();
                }
                return RedirectToAction("Index");
            }
            return View(foodCategoryDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var foodCategory = await _foodCategoryService.GetById(id);
            if (foodCategory == null)
            {
                return NotFound();
            }
            return View(foodCategory);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _foodCategoryService.Delete(id);
            return RedirectToAction("Index");
        }

    }
}
