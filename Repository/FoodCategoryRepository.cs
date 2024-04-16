using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreManagement.Data;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Models;

namespace StoreManagement.Repository
{
    public class FoodCategoryRepository : IFoodCategoryRepository
    {
        public readonly ApplicationDbContext _context;
        public FoodCategoryRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<FoodCategory> Create(FoodCategory foodCategory)
        {
            var newFoodCategory = new FoodCategory()
            {
                Name = foodCategory.Name,
                StoreId = foodCategory.StoreId
            };
            _context.Add(newFoodCategory);
            await _context.SaveChangesAsync();
            return newFoodCategory;
        }

        public async Task Delete(int id, bool incluDeleted = false)
        {
            var foodCategory = _context.FoodCategories.Where(obj => obj.Id == id && obj.IsDeleted == incluDeleted).FirstOrDefault();
            if(foodCategory != null)
            {
                foodCategory.IsDeleted = true;
                await _context.SaveChangesAsync();
            }  
        }

        public async Task<FoodCategory> Edit(int id, FoodCategory foodCategory, bool incluDeleted = false)
        {
            var foodCategories = await _context.FoodCategories.FindAsync(id);
            if (foodCategories == null)
            {
                return null;
            }
            foodCategories.Id = id;
            foodCategories.Name = foodCategory.Name;
            foodCategories.StoreId = foodCategory.StoreId;
            await _context.SaveChangesAsync();
            return foodCategories;
        }

        public async Task<List<FoodCategory>> GetAll(List<int> StoreId, bool incluDeleted = false)
        {
            List<FoodCategory> categories = new List<FoodCategory>();
            foreach (int id in StoreId)
            {
                var temp = _context.FoodCategories.Where(m => m.StoreId == id && m.IsDeleted == incluDeleted).ToList();
                if (temp != null)
                {
                    categories.AddRange(temp);
                }
            }
            if (categories.Count > 0)
            {
                return categories;

            }
            else
            {
                return null;
            }
        }

        public Task<FoodCategory> GetById(int id, bool incluDeleted = false)
        {
            var foodCategory = _context.FoodCategories.Where(obj => obj.Id == id && obj.IsDeleted == incluDeleted).FirstOrDefaultAsync();
            return foodCategory;
        }
        public List<FoodCategory> GetByStoreId(int id, bool incluDeleted = false)
        {
            List<FoodCategory> foodCategory = _context.FoodCategories.Where(obj => obj.StoreId == id && obj.IsDeleted == incluDeleted).ToList();
            return foodCategory;
        }

    }
}
