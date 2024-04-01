using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.Areas.Owner.Controllers
{
    public class ManagementOrderController : Controller
    {
        [Area("Owner")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
