﻿
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Service;
using System.Net;
using System.Net.Http;
using System.Net.WebSockets;

namespace StoreManagement.Areas.Owner.Controllers
{
    [Area("Owner")]
    [Authorize(Roles = "2")]

    public class ManagementFoodController : Controller
    {
        public readonly IFoodItemService _foodItemService;
        public readonly IFoodCategoryService _foodCategoryService;

        public ManagementFoodController(IFoodItemService foodItemService,IFoodCategoryService foodCategoryService)
        {
            this._foodItemService = foodItemService;
            this._foodCategoryService = foodCategoryService;
        }
        public async Task<IActionResult> Index()
        {
            var foodItem = await _foodItemService.GetAll();
            return View(foodItem);
        }
        [HttpGet]               
        public async Task<IActionResult> Create()
        {
            var foodCategories = await _foodCategoryService.GetAll();
            ViewBag.FoodCategories = new SelectList(foodCategories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FoodItemDTO foodItemDTO, IFormFile uFile)
        {
            if (ModelState.IsValid)
            {
                if(foodItemDTO.ImageUrl != null)
                {
                    var imagePath = await _foodItemService.SaveImages(foodItemDTO.ImageUrl,uFile);
                    foodItemDTO.ImageUrl = imagePath;

                }
                var result = await _foodItemService.Create(foodItemDTO);
                if (result != null)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                     ModelState.AddModelError(string.Empty, "Failed to create FoodItem.");
                }
            }
            var foodCategories = await _foodCategoryService.GetAll();
            ViewBag.FoodCategories = new SelectList(foodCategories, "Id", "Name");
            return View(foodItemDTO);
        }

        [HttpGet]
        public async Task<ActionResult> Update(int id)
        {
            var foodItem = await _foodItemService.GetById(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            var foodCategories = await _foodCategoryService.GetAll();
            ViewBag.FoodCategories = new SelectList(foodCategories, "Id", "Name");
            return View(foodItem);
        }

        [HttpPost]
        public async Task<ActionResult> Update(int id, FoodItemDTO foodItemDTO, IFormFile uFile)
        {
            ModelState.Remove("ImageUrl");
            if (id != foodItemDTO.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var foodItem = await _foodItemService.GetById(id);
                if (foodItemDTO.ImageUrl == null)
                {
                    foodItemDTO.ImageUrl = foodItem.ImageUrl;
                }
                if (uFile != null)
                {
                    var imagePath = await _foodItemService.SaveImages(foodItemDTO.ImageUrl, uFile);
                    foodItem.ImageUrl = imagePath;
                }
                foodItem.Name = foodItemDTO.Name;
                foodItem.Price = foodItemDTO.Price;
                foodItem.Description = foodItemDTO.Description;
                foodItem.FoodCategoryId = foodItemDTO.FoodCategoryId;
                await _foodItemService.Edit(id,foodItem, uFile);
                return RedirectToAction("Index");
            }
            var foodCategories = await _foodCategoryService.GetAll();
            ViewBag.FoodCategories = new SelectList(foodCategories,"Id","Name");
            return View(foodItemDTO);
        }


        [HttpGet]
        public async Task<ActionResult> Delete(int id)
        {
            var foodItem = await _foodItemService.GetById(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            return View(foodItem);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            await _foodItemService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
