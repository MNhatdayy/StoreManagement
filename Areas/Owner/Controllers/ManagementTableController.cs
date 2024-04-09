using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement.DTO;
using StoreManagement.DTO.AuthDTO;
using StoreManagement.Interfaces.IServices;
using System.Net;
using System.Security.Claims;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    [Authorize(Roles = "2")]

    public class ManagementTableController : Controller
    {

        private readonly ITableService _tableService;
        private readonly IStoreService _storeService;
        private int userId;


        public ManagementTableController(ITableService tableService,
                                         IStoreService storeService)
        {
            _tableService = tableService;
            _storeService = storeService;
        }

        public async Task<IActionResult> Index()
        {
            userId = int.Parse(User.Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault().Value);
            var listStore = await _storeService.GetStoresByUserId(userId);
            List<int> ints = listStore.Select(x => x.Id).ToList();
            var table = await _tableService.GetTablesByListId(ints);
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
                await _tableService.Create(tableDTO);
                return Redirect("/owner/managementtable/index");
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
                return RedirectToAction("Index");
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
            return RedirectToAction("Index");
        }
    }
}
