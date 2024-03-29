using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement.DTO;
using StoreManagement.DTO.AuthDTO;
using StoreManagement.Interfaces.IServices;
using System.Net;

namespace StoreManagement.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManagementTableController : Controller
    {

        private readonly ITableService _tableService;

        public ManagementTableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        public async Task<IActionResult> Index()
        {
            var table = await _tableService.GetAll();
            return View(table);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TableDTO tableDTO)
        {
            if(ModelState.IsValid)
            {
                await _tableService.Create(tableDTO);
                return RedirectToAction("Index");
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
