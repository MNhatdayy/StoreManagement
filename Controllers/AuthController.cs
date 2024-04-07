using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.DTO.AuthDTO;
using System.Security.Claims;
using IAuthenticationService = StoreManagement.Interfaces.IServices.IAuthenticationService;
using StoreManagement.DTO;

namespace StoreManagement.Controllers
{
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


            switch(result.RoleId) {
                case 1:
                    {
                        await CreateCookieAsync(result);
                        return Redirect("/admin/home");
                    }
                case 2:
                    {
                        await CreateCookieAsync(result);
                        return Redirect("/owner/home");
                    }
                default:
                    {

                        return Redirect("/auth/login");
                    }
            }

        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return Redirect("/auth/login");
        }

        private async Task CreateCookieAsync(AppUserDTO appUser)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, appUser.Id.ToString()),
                new Claim(ClaimTypes.Name, appUser.Id.ToString()),
                new Claim(ClaimTypes.Role, appUser.RoleId.ToString())
            };

            ClaimsIdentity identity = new ClaimsIdentity(
                claims,
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
        }
    }

}
