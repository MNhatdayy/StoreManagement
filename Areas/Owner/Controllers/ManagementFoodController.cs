
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IServices;
using System.Security.Claims;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    [Authorize(Roles = "2")]

    public class ManagementFoodController : Controller
    {
        public readonly IFoodItemService _foodItemService;
        public readonly IFoodCategoryService _foodCategoryService;
        private readonly IAppUserService _appUserService;
        private readonly IStoreService _storeService;

        public ManagementFoodController(IFoodItemService foodItemService, IFoodCategoryService foodCategoryService, IAppUserService appUserService, IStoreService storeService)
        {
            _foodItemService = foodItemService;
            _foodCategoryService = foodCategoryService;
            _appUserService = appUserService;
            _storeService = storeService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var user = await _appUserService.GetByIdAsync(userId);

            var countStore = await _storeService.GetStoresByUserId(userId);

            var foodCategory = await _foodCategoryService.GetAll(countStore.Select(x => x.Id).ToList());

            var foodItem = await _foodItemService.GetAll(foodCategory.Select(x => x.Id).ToList());

            return View(foodItem);
        }
        [HttpGet]               
        public async Task<IActionResult> Create()
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var user = await _appUserService.GetByIdAsync(userId);

            var countStore = await _storeService.GetStoresByUserId(userId);

            var foodCategory = await _foodCategoryService.GetAll(countStore.Select(x => x.Id).ToList());

            ViewBag.FoodCategories = new SelectList(foodCategory, "Id", "Name");
           
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FoodItemDTO foodItemDTO, IFormFile uFile)
        {
            if (ModelState.IsValid)
            {
                if(uFile != null && uFile.Length > 0)
                {
                    var imagePath = await _foodItemService.SaveImages(uFile);
                    foodItemDTO.ImageUrl = imagePath;

                }
                var result = await _foodItemService.Create(foodItemDTO);
                if (result != null)
                {
                    return Redirect("/owner/managementfood/index");
                }
                else
                {
                     ModelState.AddModelError(string.Empty, "Failed to create FoodItem.");
                }
            }

            var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var user = await _appUserService.GetByIdAsync(userId);
            var countStore = await _storeService.GetStoresByUserId(userId);
            var foodCategory = await _foodCategoryService.GetAll(countStore.Select(x => x.Id).ToList());
            ViewBag.FoodCategories = new SelectList(foodCategory, "Id", "Name");
            return View(foodItemDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var foodItem = await _foodItemService.GetById(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var user = await _appUserService.GetByIdAsync(userId);
            var countStore = await _storeService.GetStoresByUserId(userId);
            var foodCategory = await _foodCategoryService.GetAll(countStore.Select(x => x.Id).ToList());
            ViewBag.FoodCategories = new SelectList(foodCategory, "Id", "Name");
            return View(foodItem);
        }

        [HttpPost]
        public async Task<ActionResult> Update(int id, FoodItemDTO foodItemDTO, IFormFile uFile)
        {
            ModelState.Remove("ImageUrl");
            if (id != foodItemDTO.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var foodItem = await _foodItemService.GetById(id);
                if (foodItemDTO.ImageUrl == null)
                {
                    foodItemDTO.ImageUrl = foodItem.ImageUrl;
                }
                if (uFile != null)
                {
                    var imagePath = await _foodItemService.SaveImages(uFile);
                    foodItem.ImageUrl = imagePath;
                }
                foodItem.Name = foodItemDTO.Name;
                foodItem.Price = foodItemDTO.Price;
                foodItem.Description = foodItemDTO.Description;
                foodItem.FoodCategoryId = foodItemDTO.FoodCategoryId;
                await _foodItemService.Edit(id,foodItem, uFile);
                return Redirect("/owner/managementfood/index");

            }
            var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var user = await _appUserService.GetByIdAsync(userId);
            var countStore = await _storeService.GetStoresByUserId(userId);
            var foodCategory = await _foodCategoryService.GetAll(countStore.Select(x => x.Id).ToList());
            ViewBag.FoodCategories = new SelectList(foodCategory, "Id", "Name");
            return View(foodItemDTO);
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var foodItem = await _foodItemService.GetById(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            return View(foodItem);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _foodItemService.Delete(id);
            return Redirect("/owner/managementfood/Index");
        }
    }
}
