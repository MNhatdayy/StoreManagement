using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagementStoreController : Controller
    {
        [HttpGet]

        public IActionResult Index()
        {
            return View();
        }
    }
}
