using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IServices;
using System.Security.Claims;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    [Authorize(Roles = "2")]
    public class ManagementInvoiceController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IInvoiceService _invoiceService;
        private readonly ITableService _tableService;
        private readonly IStoreService _storeService;
        int storeId;
        public ManagementInvoiceController(IOrderService orderService, IInvoiceService invoiceService, ITableService tableService, IStoreService storeService)
        {
            _orderService = orderService;
            _invoiceService = invoiceService;
            _tableService = tableService;
            _storeService = storeService;
        }
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var listStore = await _storeService.GetStoresByUserId(userId);
            storeId = listStore.Select(x => x.Id).FirstOrDefault();

            var order = await _orderService.GetListOrderAccept(storeId);
            ViewBag.orderList = order;
            ViewBag.idStore = storeId;
            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> GetListOrderAccept(int id)
        {
            var order = await _orderService.GetListOrderAccept(id);
            ViewBag.IdStore = storeId;
            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var order = await _orderService.GetOrderAcceptedById(id);
            var orderDetails = await _orderService.GetListOrderDetailsByIdOrder(id);
            ViewBag.OrderDetail = orderDetails;
            if (order == null)
            {
                return NotFound();
            }
            ViewBag.Id = order.Id;
            ViewBag.order = order;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CompleteInvoice(InvoiceDTO invoiceDTO)
        {
            if (ModelState.IsValid)
            {
                var order = await _orderService.GetOrderAcceptedById(invoiceDTO.OrderId);
                await _orderService.UpdatePayAsync(invoiceDTO.OrderId);
                invoiceDTO.Status = true;
                if (invoiceDTO.Charge > 0)
                {
                    invoiceDTO.TotalPrice = order.TotalPrice + invoiceDTO.Charge;
                }
                invoiceDTO.PayTime = DateTime.UtcNow;
                await _tableService.UpdateStatus(order.TableId);
                var result = await _invoiceService.CreateAsync(invoiceDTO);
                
                if (result != null)
                {
                    
                    return Redirect("/owner/ManagementInvoice/ListInvoice"); 
                }
                else
                {
                    return NotFound();
                }     
            }
            return View(invoiceDTO);
        }
        [HttpGet]
        public async Task<IActionResult> ListInvoice()
        {
            var userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var listStore = await _storeService.GetStoresByUserId(userId);
            storeId = listStore.Select(x => x.Id).FirstOrDefault();
            var list = await _invoiceService.GetAllPaidAsync(storeId);
            
            return View(list);      
        }
        [HttpGet]
        public async Task<IActionResult> InvoiceDetails(int id)
        {
            try
        {
                var invoice = await _invoiceService.GetInvoicePaidById(id);

                if (invoice == null)
                {
                    return NotFound(); 
                }

                var order = await _orderService.GetOrderPaidAsync(invoice.Order.Table.StoreID, invoice.Order.TableId);

                if (order == null)
                {
                    return NotFound(); 
                }

                var orderDetail = await _orderService.GetListOrderDetailsByIdOrder(order.Id);

                ViewBag.Order = order;
                ViewBag.OrderDetail = orderDetail;

                return View(invoice); 
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }
        [HttpGet]
        public async Task<IActionResult> DetailUnpaid(int id)
        {
            var order = await _orderService.GetOrderAcceptedById(id);
            if (order == null)
            {
                return NotFound();
            }
            var orderDetail = await _orderService.GetListOrderDetailsByIdOrder(order.Id);
            ViewBag.Order = order;
            ViewBag.OrderDetail = orderDetail;
            return View(order);
        }
    }
}
