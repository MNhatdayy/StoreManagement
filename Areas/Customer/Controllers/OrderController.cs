using Microsoft.AspNetCore.Mvc;

namespace StoreManagement.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        [HttpGet]
        [Route("/customer/order/{idStore}/{idTable}")]
        public IActionResult Index(int idStore, int idTable)
        {
            ViewBag.IdStore = idStore;
            ViewBag.IdTable = idTable;
            return View();
        }
    }
}
