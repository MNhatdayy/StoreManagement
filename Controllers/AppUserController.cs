using Microsoft.AspNetCore.Mvc;
using StoreManagement.Interfaces.IServices;

namespace StoreManagement.Controllers
{
    public class AppUserController : Controller
    {
        private readonly IAppUserService _appUserServices;
        public AppUserController(IAppUserService services)
        {
            _appUserServices = services;

        }
        public async Task<IActionResult> AppUser()
        {
            var appUser = await _appUserServices.GetAllAsync();
            return View(appUser);
        }
    }
}
