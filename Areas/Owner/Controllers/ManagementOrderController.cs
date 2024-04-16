using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Interfaces.IServices;
using System.Security.Claims;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    [Authorize(Roles = "2")]
    public class ManagementOrderController : Controller
    {
        
        private readonly IOrderService _orderService;
        private readonly ITableService _tableService;
        private readonly IStoreService _storeService;

        public ManagementOrderController(IOrderService orderService, ITableService tableService, IStoreService storeService)
        {
            _orderService = orderService;
            _tableService = tableService;
            _storeService = storeService;
        }
        [HttpGet]//ghi ra cho dep
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var listStore = await _storeService.GetStoresByUserId(userId);
            int storeId = listStore.Select(x => x.Id).FirstOrDefault();

            var order = await _orderService.GetListOrder(storeId);
            ViewBag.orderList = order;
            ViewBag.idStore = storeId;
            return View(order);
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
            var orderDetail = await _orderService.GetListOrderDetailsByIdOrder(order.Id);
            await _tableService.UpdateStatus(order.TableId);
            ViewBag.orderDetail = orderDetail;
            return View(orderAccepted);
        }
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var order = await _orderService.GetOrderById(id);
            var orderDetail = await _orderService.GetListOrderDetailsByIdOrder(id);
            ViewBag.OrderDetails = orderDetail;
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Id = order.Id;
            return View(order);
        }
        /*[HttpGet]
        public async Task<IActionResult> GetListOrderAccept(int id)
        {
            var order = await _orderService.GetListOrderAccept(id);
            ViewBag.IdStore = 1;
            return View(order);
        }*/
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
