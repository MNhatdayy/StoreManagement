using Microsoft.AspNetCore.Mvc;
using StoreManagement.Controllers;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class ManagementOrderController : Controller
    {
        int idStore = 1;
        private readonly IOrderService _orderService;

        public ManagementOrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]//ghi ra cho dep
        public async Task<IActionResult> Index()
        {
            var order = await _orderService.GetListOrder(1);
            ViewBag.orderList = order;
            ViewBag.idStore = idStore;
            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> Reload(int idStore) 
        {
            var order = await _orderService.GetListOrder(idStore);
            ViewBag.idStore = idStore;
            return RedirectToAction("Index", order);
        }
        [HttpGet]
        public async Task<IActionResult> Accept(int id)
        {
            var order = await _orderService.GetOrderById(id);
            if(order == null)
            {
                return View("Error");
            }
            var orderupdate = await _orderService.UpdateAsync(id);
            var orderAccepted = await _orderService.GetOrderAcceptedById(order.Id);
            var ordderDetail = await _orderService.GetListOrderDetailsByIdOrder(order.Id);
            ViewBag.ordderDetail = ordderDetail;
            return View(orderAccepted);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var order = await _orderService.GetOrderById(id);
            var ordderDetail = await _orderService.GetListOrderDetailsByIdOrder(id);
            ViewBag.OrderDetails = ordderDetail;
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Id = order.Id;
            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> GetListOrderAccept(int id)
        {
            var order = await _orderService.GetListOrderAccept(id);
            ViewBag.IdStore = 1;
            return View(order);
        }
        public async Task<IActionResult> DetailAccepted(int id)
        {
            var order = await _orderService.GetOrderAcceptedById(id);
            var ordderDetail = await _orderService.GetListOrderDetailsByIdOrder(id);
            ViewBag.OrderDetails = ordderDetail;
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> Denied(int id)
        {
            await _orderService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
