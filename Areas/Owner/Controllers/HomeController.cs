using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;
using StoreManagement.Service;
using System.Diagnostics;
using System.Security.Claims;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    [Authorize(Roles = "2")]
    public class HomeController : Controller
    {
        private int userId;
        
        private readonly IStoreService _storeService;
        private readonly IAppUserService _appUserService;
        private readonly ITableService _tableService;

        public HomeController(IStoreService storeService,
                              IAppUserService appUserService,
                              ITableService tableService)
        {
            _storeService = storeService;
            _appUserService = appUserService;
            _tableService = tableService;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            // Get claims Cookie
            userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);

            var user = await _appUserService.GetByIdAsync(userId);

            var countStore = await _storeService.GetStoresByUserId(userId);

            var table = await _tableService.GetTablesByListId(countStore.Select(x => x.Id).ToList());

            int tableBlank = table.Where(x => x.Status == false).ToList().Count;

            ViewBag.tableBlank = tableBlank;
            ViewBag.countTable = table.Count; 
            ViewBag.countStore = countStore.Count;   
            ViewBag.username = user.Name;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}