using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.Areas.Admin.Controllers
{
    public class ManagementTableController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
