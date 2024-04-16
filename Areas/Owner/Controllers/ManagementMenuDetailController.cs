using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IServices;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    [Authorize(Roles = "2")]
    public class ManagementMenuDetailController : Controller
    {
        private readonly IMenuDetailService _menuDetailService;
        private readonly IMenuService _menuService;
        public readonly IFoodItemService _foodItemService;
        public readonly IFoodCategoryService _foodCategoryService;
        private readonly IAppUserService _appUserService;
        private readonly IStoreService _storeService;
        public ManagementMenuDetailController(IMenuDetailService menuDetailService,
            IMenuService menuService,
            IFoodItemService foodItemService,
            IFoodCategoryService foodCategoryService,
            IAppUserService appUserService,
            IStoreService storeService)
        {
            _menuDetailService = menuDetailService;
            _menuService = menuService;
            _foodItemService = foodItemService;
            _foodCategoryService = foodCategoryService;
            _appUserService = appUserService;
            _storeService = storeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var menuDetail = await _menuDetailService.GetAll();
            return View(menuDetail);
        }

        [HttpGet]
        [Route("/owner/managementmenudetail/getmenuitem/{menuId:int}")]
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
        [Route("/owner/managementmenudetail/addmenuitem/{menuId:int}")]

        public async Task<IActionResult> AddMenuItem(int menuId)
        {
            try
            {
                var menu = await _menuService.GetById(menuId);
                if (menu == null)
                {
                    return BadRequest("Invalid menu ID");
                }

                var storeId = menu.StoreId;
                ViewBag.StoreId = storeId;
                ViewBag.MenuId = menu.Id;

                var foodCategory = await _foodCategoryService.GetByStoreId(storeId);


                var foodItems = await _foodItemService.GetAll(foodCategory.Select(x => x.Id).ToList());
                var filteredFoodItems = foodItems.Where(item => foodCategory.Any(fc => fc.Id == item.FoodCategoryId && fc.StoreId == storeId));
                ViewBag.FoodItems = filteredFoodItems.Select(item => new SelectListItem { Value = item.Id.ToString(), Text = item.Name }).ToList();

                var menuDetails = await _menuDetailService.GetMenuDetailsByMenuId(menuId);
                ViewBag.MenuDetails = menuDetails;
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = "Có lỗi xảy ra: " + ex.Message });
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

                return Redirect("/owner/managementmenudetail/GetMenuItem/" + menuId);

            }
            catch (Exception ex)
            {
                return BadRequest(new { errorMessage = "Có lỗi xảy ra: " + ex.Message });
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
            return Redirect("/owner/managementmenudetail/GetMenuItem/" + menuId);
        }

        [HttpPost]
        public IActionResult UpdateMenuStatus( [FromBody]List<MenuDetailDTO> updatedStatusList)
        {
            try
            {
                foreach (var item in updatedStatusList)
                {
                    _menuDetailService.UpdateMenuStatus(item.MenuId, item.FoodItemId, item.Status);
                }

                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}
