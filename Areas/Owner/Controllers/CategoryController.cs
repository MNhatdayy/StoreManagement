using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IServices;
using System.Security.Claims;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class CategoryController : Controller
    {
        public readonly IFoodCategoryService _foodCategoryService;
        private readonly IAppUserService _appUserService;
        private readonly IStoreService _storeService;
        public CategoryController(IFoodCategoryService foodCategoryService, IStoreService storeService, IAppUserService appUserService)
        {
            _foodCategoryService = foodCategoryService;
            _storeService = storeService;
            _appUserService = appUserService;
        }
        public async Task<ActionResult> Index()
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var user = await _appUserService.GetByIdAsync(userId);

            var countStore = await _storeService.GetStoresByUserId(userId);
            var foodCategory = await _foodCategoryService.GetAll(countStore.Select(x => x.Id).ToList());

            return View(foodCategory);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var listStore = await _storeService.GetStoresByUserId(userId);

            ViewBag.List = new SelectList(listStore, "Id", "StoreName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FoodCategoryDTO foodCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _foodCategoryService.Create(foodCategoryDTO);
                return Redirect("/owner/category/index");
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
                return Redirect("/owner/category/index");
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
            return Redirect("/owner/category/index");
        }

    }
}
