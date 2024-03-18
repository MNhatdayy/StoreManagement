using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.Areas.Admin.Controllers
{
    public class ManagementOrderController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
