using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StoreManagement.Controllers;
using StoreManagement.DTO;
using StoreManagement.Helper;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;
using StoreManagement.Service;
using System.Net.Http;
using System.Text;

namespace StoreManagement.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderController : Controller
    {
        int idStore = 1;
        int idTable = 1;
        private readonly IMenuDetailService _menuDetail;
        private readonly IMenuService _menuService;
        private readonly IFoodItemService _foodService;
        private readonly HttpClient _httpClient;
        private readonly IOrderService _orderService;

        public OrderController(IMenuDetailService menuDetail,
                                IMenuService menuService,
                                 IFoodItemService foodItemService,
                                 HttpClient httpClient,
                                 IOrderService orderService
                                ) { 
            _menuDetail = menuDetail;
            _menuService = menuService;
            _foodService = foodItemService;
            _httpClient = httpClient;
            _orderService = orderService;
        }
        //--------------------- Cart-----------------------------//
        public List<CartItem> Carts
        {
            get
            {
                var data = HttpContext.Session.Get<List<CartItem>>("Cart");
                if (data == null)
                {
                    data = new List<CartItem>();
                }
                return data;
            }
        }
        [Route("/customer/order/ViewCart/1/1")]
        public IActionResult ViewCart(int idTbale, int idStore)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
            return View(cart);
        }

        public async Task<IActionResult> AddToCart(int foodId, int quantity)
        {
            
            var foodItem = await _foodService.GetById(foodId);


            if (foodItem == null)
            {
                return RedirectToAction("Index", "Order", new { idStore, idTable});
            }
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart") ?? new ShoppingCart();
            var cartItem = new CartItem
            {
                IdFood = foodId,
                NameFood = foodItem.Name,
                Price = foodItem.Price,
                Quantity = quantity,
                Picture = foodItem.ImageUrl,
            };
            cart.AddItem(cartItem);

            HttpContext.Session.Set("Cart", cart);

            return RedirectToAction("Index", "Order", new { idStore , idTable });
        }

        public async Task<IActionResult> UpdateCart(int foodId, int quantity)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart");
            var itemToUpdate = cart.Items.FirstOrDefault(item => item.IdFood == foodId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Quantity = quantity;
            }
            HttpContext.Session.Set("Cart", cart);
            return Ok();
        }
        public IActionResult DeleteFromCart(int foodId)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart");
            var itemToDelete = cart.Items.FirstOrDefault(obj => obj.IdFood == foodId);
            if (itemToDelete != null)
            {
                cart.Items.Remove(itemToDelete);
                HttpContext.Session.Set("Cart", cart);
            }
            return RedirectToAction("ViewCart", "Order", new { idStore, idTable });
        }
        [HttpPost]
        public IActionResult SaveNotes(int foodId, string notes)
        {
            var cart = HttpContext.Session.Get<ShoppingCart>("Cart");
            var itemToUpdate = cart.Items.FirstOrDefault(item => item.IdFood == foodId);
            if (itemToUpdate != null)
            {
                itemToUpdate.Notes = notes;
                HttpContext.Session.Set("Cart", cart);
            }
            return Ok(); 
        }
        [HttpPost]
        public async Task<IActionResult> SubmitOrderAsync(OrderDTO order)
        {
            try
            {

                var cart = HttpContext.Session.Get<ShoppingCart>("Cart");
                if (cart == null || !cart.Items.Any())
                {
                    return RedirectToAction("Index");
                }
                order.TableId = idTable;
                order.Created = DateTime.UtcNow;
                order.TotalPrice = cart.Items.Sum(i => i.Price * i.Quantity);
                order.status = false;
                order.OrderDetails = cart.Items.Select(i => new OrderDetailsDTO
                {
                    FoodId = i.IdFood,
                    Quantity = i.Quantity,
                    Price = i.Price,
                }).ToList();
                await _orderService.CreateAsync(order);

                HttpContext.Session.Remove("Cart");

                return RedirectToAction("Index","Order" , new { idStore,idTable }); // Trang xác nhận hoàn thành đơn hàng
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        //---------------------End Cart-----------------------------//
        [HttpGet]
        [Route("/customer/order/{idStore}/{idTable}")]
        public async Task<IActionResult> Index(int idStore, int idTable)
        {
            var menu = await _menuService.GetMenuByIdStore(idStore);
            var menuDetail = await _menuDetail.GetMenuDetailsByMenuId(menu.Id);
            ViewBag.menuDetail = menuDetail;
            return View(menuDetail);
        }
    }
}
