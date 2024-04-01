using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class ManagementStoreController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IAppUserService _appUserService;

        public ManagementStoreController(IStoreService storeService, IAppUserService appUserService)
        {
            _storeService = storeService;
            _appUserService = appUserService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var store = await _storeService.GetAllAsync();
            var appUsers = await _appUserService.GetAllAsync();
            ViewData["AppUsers"] = appUsers;
            return View(store);
        }
        
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var store = await _storeService.GetByIdAsync(id);
            if(store == null)
            {
                return NotFound();
            }
            return View(store);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, StoreDTO storeDTO)
        {
            if(id != storeDTO.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                await _storeService.UpdateAsync(storeDTO);
                return RedirectToAction("Index");
            }
            return View(storeDTO);
        }
        
        [HttpPost, ActionName("Search")]
        public async Task<IActionResult> Search(string searchTerm)
        {
            var store = await _storeService.GetByNameAsync(searchTerm);
            if(store == null)
            {
                return NotFound(searchTerm);
            }
            var appUsers = await _appUserService.GetAllAsync();
            ViewData["AppUsers"] = appUsers;
            return View(store);
        }
    }
}
