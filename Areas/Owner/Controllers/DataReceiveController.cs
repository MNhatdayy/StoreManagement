using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class DataReceiveController : Controller
    {
        public IActionResult Index()
        {
            int idStore = HttpContext.Session.GetInt32("idStore") ?? 0;
            int idTable = HttpContext.Session.GetInt32("idTable") ?? 0;
            int listFood = HttpContext.Session.GetInt32("listFood") ?? 0;

            ViewBag.IdStore = idStore;
            ViewBag.IdTable = idTable;
            ViewBag.ListFood = listFood;

            return View();
        }
    }
}
