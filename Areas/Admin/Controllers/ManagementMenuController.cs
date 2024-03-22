using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagementMenuController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
