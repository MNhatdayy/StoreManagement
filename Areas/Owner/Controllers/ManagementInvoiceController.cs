using Microsoft.AspNetCore.Mvc;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    public class ManagementInvoiceController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IInvoiceService _invoiceService;
        private readonly ITableService _tableService;
        int idStore = 1;
        int idTable = 1;
        public ManagementInvoiceController(IOrderService orderService, IInvoiceService invoiceService, ITableService tableService)
        {
            _orderService = orderService;
            _invoiceService = invoiceService;
            _tableService = tableService;
        }
        public async Task<IActionResult> Index()
        {
            var order = await _orderService.GetListOrderAccept(1);
            ViewBag.orderList = order;
            ViewBag.idStore = idStore;
            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> GetListOrderAccept(int id)
        {
            var order = await _orderService.GetListOrderAccept(id);
            ViewBag.IdStore = 1;
            return View(order);
        }
        [HttpGet]
        public async Task<IActionResult> Create(int id)
        {
            var order = await _orderService.GetOrderAcceptedById(id);
            var ordderDetail = await _orderService.GetListOrderDetailsByIdOrder(id);
            ViewBag.OrderDetails = ordderDetail;
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
                invoiceDTO.TotalPrice =  order.TotalPrice + invoiceDTO.Charge;
                invoiceDTO.PayTime = DateTime.UtcNow;
                await _tableService.UpdateStatus(order.TableId);
                var result = await _invoiceService.CreateAsync(invoiceDTO);
                
                if (result != null)
                {
                    
                    return View("Index"); 
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
            var list = await _invoiceService.GetAllPaidAsync(idStore);
            
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
