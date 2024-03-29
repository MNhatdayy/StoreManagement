using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Service;

namespace StoreManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagementMenuController : Controller
    {
        private readonly IMenuService _menuService;
        public readonly IFoodItemService _foodItemService;
        public ManagementMenuController(IMenuService menuService, IFoodItemService foodItemService)
        {
            _menuService = menuService;
            _foodItemService = foodItemService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var menulist = await _menuService.GetAll();
            return View(menulist);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MenuDTO menuDTO)
        {
            if (ModelState.IsValid)
            {
                await _menuService.Create(menuDTO);
                return RedirectToAction("Index");
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
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }
    }
}
