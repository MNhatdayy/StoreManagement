
using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.Areas.Admin.Controllers
{
    public class ManagementFoodController : Controller
    {
        [Area("Admin")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
