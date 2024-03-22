using Microsoft.AspNetCore.Mvc;
using StoreManagement.DTO.AuthDTO;
using StoreManagement.Interfaces.IServices;

namespace StoreManagement.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly IAuthenticationService _authService;


        public AuthController(IAuthenticationService authenticationService)
        {
            _authService = authenticationService;
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await _authService.Login(loginDTO);
            if (result == -1)
            {
                return RedirectToAction("Login", "Login");
            }
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Login()
        {
            return View();
        }
    }

}
