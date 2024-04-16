using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;

namespace StoreManagement.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "1")]

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
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(AppUser user)
        {
            if (!ModelState.IsValid)
            {
                // Trả về view với thông báo lỗi validation
                return View(user);
            }

            var exists = await _userService.GetByEmailAsync(user.Email);
            if (exists != null)
            {
                ViewBag.Error = "Người dùng đã tồn tại";
                return View(user);
            }
            await _userService.CreateAsync(user);
            return Redirect("/admin/managementappuser/index");

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
                return Redirect("/admin/managementappuser/index");

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
        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _userService.Delete(id);
            return Redirect("/admin/managementappuser/index");

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
