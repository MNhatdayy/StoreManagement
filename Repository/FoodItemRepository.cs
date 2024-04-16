using Microsoft.EntityFrameworkCore;
using StoreManagement.Data;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Models;

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
            foodItems.Name = foodItem.Name;
            foodItems.Price = foodItem.Price;
            foodItems.Description = foodItem.Description;
            foodItems.FoodCategory = foodItem.FoodCategory;
            foodItems.ImageUrl = foodItem.ImageUrl;

            await _context.SaveChangesAsync();
            return foodItems;
        }

        public async Task<List<FoodItem>> GetAll(List<int> CategoryId, bool incluDeleted = false)
        {
            List<FoodItem> food = new List<FoodItem>();
            foreach (int id in CategoryId)
            {
                var temp = _context.FoodItems.Where(m => m.FoodCategoryId == id && m.IsDeleted == false).ToList();
                if (temp != null)
                {
                    foreach (var item in temp)
                    {
                        food.Add(item);
                    }
                }
            }
            if (food.Count > 0)
            {
                return food;
            }
            else
            {
                return null;
            }          
        }

        public async Task<FoodItem> GetById(int id, bool incluDeleted = false)
        {
            return await _context.FoodItems.Include("FoodCategory").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<string> SaveImage(IFormFile uFile)
        {

            if (uFile != null && uFile.Length >0)
            {
                var savePath = Path.Combine("wwwroot/images/food", uFile.FileName); 
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    await uFile.CopyToAsync(fileStream);
                }
                return "/images/food/" + uFile.FileName;
            }
            return null; 
        }
    }
}
