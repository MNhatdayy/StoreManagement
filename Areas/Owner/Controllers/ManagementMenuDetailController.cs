using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;
using StoreManagement.Service;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class ManagementMenuDetailController : Controller
    {
        private readonly IMenuDetailService _menuDetailService;
        private readonly IMenuService _menuService;
        public readonly IFoodItemService _foodItemService;
        public readonly IFoodCategoryService _foodCategoryService;
        public ManagementMenuDetailController(IMenuDetailService menuDetailService, 
            IMenuService menuService, 
            IFoodItemService foodItemService, 
            IFoodCategoryService foodCategoryService)
        {
            _menuDetailService = menuDetailService;
            _menuService = menuService;
            _foodItemService = foodItemService;
            _foodCategoryService = foodCategoryService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var menuDetail = await _menuDetailService.GetAll();
            return View(menuDetail);
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuItem(int menuId)
        {
            try
            {
                var menuItems = await _menuDetailService.GetMenuDetailsByMenuId(menuId);
                ViewBag.MenuId = menuId;
                return View(menuItems);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet]
        public async Task<IActionResult> AddMenuItem(int menuId)
        {
            try
            {
                var foodCategory = await _foodCategoryService.GetAll();
                var storeId = foodCategory.FirstOrDefault()?.StoreId;

                ViewBag.StoreId = storeId;

                var foodItems = await _foodItemService.GetAll();
                var filteredFoodItems = foodItems.Where(item => foodCategory.Any(fc => fc.Id == item.FoodCategoryId && fc.StoreId == storeId));
                
                ViewBag.FoodItems = filteredFoodItems.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();
                
                var menuDetails = await _menuDetailService.GetMenuDetailsByMenuId(menuId);
                
                ViewBag.MenuDetails = menuDetails;
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddMenuItem(int menuId, int foodItemId)
        {
            if (menuId <= 0 || foodItemId <= 0)
            {
                return BadRequest("Invalid");
            }
            try
            {
                var menu = await _menuService.GetById(menuId);
                if (menu == null)
                {
                    return BadRequest("Invalid menu ID");
                }
                var fooditem = await _foodItemService.GetById(foodItemId);
                if (fooditem == null)
                {
                    return BadRequest("Invalid food ID");
                }
                var foodCategory = await _foodCategoryService.GetById(fooditem.FoodCategoryId);
                if (foodCategory == null || foodCategory.StoreId != menu.StoreId)
                {
                    return BadRequest("Selected food item does not belong to the store");
                }
                var result = await _menuDetailService.AddMenuItem(menuId, foodItemId);

                if (!result)
                {
                    return BadRequest("Failed to add menu item");
                }

                return RedirectToAction("GetMenuItem", new { menuId = menuId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int menuId, int foodItemId)
        {
            var menuDetail = await _menuDetailService.GetByMenuIdAndFoodItemId(menuId, foodItemId);
            if (menuDetail == null)
            {
                return NotFound();
            }
            return View(menuDetail);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int menuId, int foodItemId)
        {
            await _menuDetailService.Delete(menuId, foodItemId);
            return RedirectToAction("GetMenuItem", new { menuId = menuId });
        }
    }
}
