using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;
using System.Diagnostics;

namespace StoreManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "1")]
    public class HomeController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IStoreService _storeService;

        public HomeController(IStoreService storeService, IAppUserService appUserService)
        {
            _storeService = storeService;
            _appUserService = appUserService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var listUser = await _appUserService.GetAllAsync();
            var listStore = await _storeService.GetAllAsync();

            ViewBag.User = listUser;
            ViewBag.Store = listStore;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("auth/login");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}