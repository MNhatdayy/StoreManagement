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

    public class ManagementMenuController : Controller
    {
        private readonly IMenuService _menuService;
        private readonly IFoodItemService _foodItemService;
        private readonly IAppUserService _appUserService;
        private readonly IStoreService _storeService;
        public ManagementMenuController(IMenuService menuService,
                                        IFoodItemService foodItemService,
                                        IAppUserService appUserService,
                                        IStoreService storeService)
        {
            _menuService = menuService;
            _foodItemService = foodItemService;
            _appUserService = appUserService;
            _storeService = storeService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var user = await _appUserService.GetByIdAsync(userId);

            var countStore = await _storeService.GetStoresByUserId(userId);

            var menulist = await _menuService.GetAll(countStore.Select(x => x.Id).ToList());
            return View(menulist);
        }
        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var user = await _appUserService.GetByIdAsync(userId);

            var countStore = await _storeService.GetStoresByUserId(userId);
            ViewBag.List = new SelectList(countStore, "Id", "StoreName");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuDTO menuDTO)
        {
            if (ModelState.IsValid)
            {
                await _menuService.Create(menuDTO);
                return Redirect("/owner/managementmenu/index");
            }
            return View(menuDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id) 
        {
            var menu = await _menuService.GetById(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        [HttpPost]
        public async Task<ActionResult> Update(int id, MenuDTO menuDTO)
        {
            if (id != menuDTO.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var result = await _menuService.Edit(id, menuDTO);
                if (result == null)
                {
                    return NotFound();
                }
                return Redirect("/owner/managementmenu/index");


            }
            return View(menuDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id) 
        {
            var menu = await _menuService.GetById(id);
            if (menu == null)
            {
                return NotFound();
            }
            return View(menu);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _menuService.Delete(id);
            return Redirect("/owner/managementmenu/index");


        }
    }
}
