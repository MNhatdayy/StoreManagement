using IronBarCode;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IServices;
using System.Security.Claims;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    [Authorize(Roles = "2")]

    public class ManagementTableController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ITableService _tableService;
        private readonly IStoreService _storeService;
        private int userId;


        public ManagementTableController(ITableService tableService,
                                         IStoreService storeService,
                                         IWebHostEnvironment env)
        {
            _tableService = tableService;
            _storeService = storeService;
            _env = env;
        }

        public async Task<IActionResult> Index()
        {
            userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var listStore = await _storeService.GetStoresByUserId(userId);
            List<int> ints = listStore.Select(x => x.Id).ToList();
            var table = await _tableService.GetTablesByListId(ints);
            var path = _env.WebRootPath;
            ViewBag.Path = path;
            return View(table);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var listStore = await _storeService.GetStoresByUserId(userId);

            ViewBag.List = new SelectList(listStore, "Id", "StoreName");
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> Create(TableDTO tableDTO)
        {
            if(ModelState.IsValid)
            {
                var table = await _tableService.Create(tableDTO);
                GenerateQR(table.StoreID, table.Id);
                return Redirect("/owner/ManagementTable/index");
            }
            return View(tableDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var table = await _tableService.GetById(id);
            if (table == null)
            {
                return NotFound();
            }
            return View(table);
        }

        [HttpPost]
        public async Task<ActionResult> Update(int id, TableDTO tableDTO)
        {
            if (id != tableDTO.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var result = await _tableService.Edit(id, tableDTO);
                if (result == null)
                {
                    return NotFound();
                }
                return Redirect("/owner/ManagementTable/index");
            }
            return View(tableDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var table = await _tableService.GetById(id);
            if(table == null)
            {
                return NotFound();
            }
            return View(table);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _tableService.Delete(id);
            return Redirect("/owner/ManagementTable/index");
        }

        private void GenerateQR(int idStore, int idTable)
        {
            string url = "http://thien027170-001-site1.gtempurl.com/customer/order/" + idStore + "/" + idTable;
            string path = "wwwroot/images/QRTable/";
            string fileName = idTable.ToString() + ".png";
            // Tạo mã QR từ URL được cung cấp
            GeneratedBarcode barcode = QRCodeWriter.CreateQrCode(url, 250);
            barcode.SetMargins(10);

            barcode.SaveAsImage(path + fileName);

            Path.Combine(path, fileName);

        }
    }
}
