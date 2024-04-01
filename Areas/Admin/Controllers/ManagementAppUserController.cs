using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Service;

namespace StoreManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagementAppUserController : Controller
    {
        private readonly IAppUserService _userService;

        public ManagementAppUserController(IAppUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetAllAsync();
            return View(user);
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AppUserDTO userDTO)
        {
            if (ModelState.IsValid)
            {
                await _userService.CreateAsync(userDTO);
                return RedirectToAction("Index");
            }

            return View(userDTO);
        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var user = await _userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, AppUserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                await _userService.UpdateAsync(userDTO);
                return RedirectToAction("Index");
            }
            return View(userDTO);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var store = await _userService.GetByIdAsync(id);
            if (store == null)
            {
                return NotFound();
            }
            return View(store);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.Delete(id);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Search(string searchTerm)
        {
            var user = await _userService.GetByNameAsync(searchTerm);
            if (user == null)
            {
                return NotFound(searchTerm);
            }
            return View(user);
        }
    }
}
