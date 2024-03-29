using Microsoft.EntityFrameworkCore;
using StoreManagement.Data;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Models;
using System.Net.WebSockets;

namespace StoreManagement.Repository
{
    public class FoodItemRepository : IFoodItemRepository
    {
        public readonly ApplicationDbContext _context;
        public FoodItemRepository(ApplicationDbContext context) 
        {
            this._context = context;
        }

        public async Task<FoodItem> Create(FoodItem foodItem)
        {
            _context.FoodItems.Add(foodItem);
            await _context.SaveChangesAsync();
            return foodItem;
        }

        public async Task Delete(int id, bool incluDeleted = false)
        {
            var foodItem = _context.FoodItems.Where(obj => obj.Id == id && obj.IsDeleted == incluDeleted).FirstOrDefault();
            if(foodItem != null)
            {
                foodItem.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<FoodItem> Edit(int id, FoodItem foodItem, IFormFile uFile, bool incluDeleted = false)
        {
            var foodItems = await _context.FoodItems.FindAsync(id);
            if (foodItems == null)
            {
                return null;
            }
            foodItems.Id = id;
            foodItems.Name = foodItem.Name;
            foodItems.Price = foodItem.Price;
            foodItems.Description = foodItem.Description;
            foodItems.FoodCategory = foodItem.FoodCategory;
            if (foodItem.ImageUrl != null)
            {
                foodItem.ImageUrl = await SaveImage(foodItem.ImageUrl,uFile);
            }
            await _context.SaveChangesAsync();
            return foodItems;
        }

        public async Task<List<FoodItem>> GetAll(bool incluDeleted = false)
        {
            if (!incluDeleted)
            {
                return await _context.FoodItems
                    .Include("FoodCategory")
                    .Where(m => !m.IsDeleted)
                    .ToListAsync();
            }
            else
            {
                return await _context.FoodItems
                    .Include("FoodCategory")
                    .ToListAsync();
            }
        }

        public async Task<FoodItem> GetById(int id, bool incluDeleted = false)
        {
            return await _context.FoodItems.Include("FoodCategory").FirstOrDefaultAsync(x => x.Id == id);
        }
        public async Task<string> SaveImage(string url, IFormFile uFile)
        {
            if (uFile != null && uFile.Length >0)
            {
                var fileName = Path.GetFileName(uFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\images",fileName);
                using(var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await uFile.CopyToAsync(fileStream);
                }
                return "/images/food/donuong/" + url;

            }
            return "/images/food/donuong/" + url; 
        }
    }
}
