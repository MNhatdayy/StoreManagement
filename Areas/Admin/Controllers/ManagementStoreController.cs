using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;

namespace StoreManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
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
        public async Task<IActionResult> Add()
        {
            var appUsers = await _appUserService.GetAllAsync();
            ViewBag.AppUsers = new SelectList(appUsers, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(StoreDTO storeDTO)
        {
            if(ModelState.IsValid)
            {
                await _storeService.CreateAsync(storeDTO);
                return RedirectToAction("Index");
            }
            var appUsers = await _appUserService.GetAllAsync();
            ViewBag.AppUsers = new SelectList(appUsers, "Id", "Name");
            return View(storeDTO);
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
        public async Task<IActionResult> Delete(int id)
        {
            var store = await _storeService.GetByIdAsync(id);
            if(store == null)
            {
                return NotFound();
            }
            return View(store);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _storeService.Delete(id);
            return RedirectToAction(nameof(Index));
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
