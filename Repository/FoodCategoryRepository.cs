using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreManagement.Data;
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
                Name = foodCategory.Name
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
            await _context.SaveChangesAsync();
            return foodCategories;
        }

        public async Task<List<FoodCategory>> GetAll(bool incluDeleted = false)
        {
            if (!incluDeleted)
            {
                var list = new List<FoodCategory>();
                list = await _context.FoodCategories.Where(m => !m.IsDeleted).ToListAsync();
                return list.ToList();
            }
            else
            {
                var list = new List<FoodCategory>();
                list = await _context.FoodCategories.ToListAsync();
                return list.ToList();
            }
        }

        public Task<FoodCategory> GetById(int id, bool incluDeleted = false)
        {
            var foodCategory = _context.FoodCategories.Where(obj => obj.Id == id && obj.IsDeleted == incluDeleted).FirstOrDefaultAsync();
            return foodCategory;
        }
    }
}
