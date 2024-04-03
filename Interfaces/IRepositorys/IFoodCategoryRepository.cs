using StoreManagement.Models;

namespace StoreManagement.Interfaces.IRepositorys
{
    public interface IFoodCategoryRepository
    {
        Task<FoodCategory> Create(FoodCategory foodCategory);
        Task<List<FoodCategory>> GetAll(bool incluDeleted = false);
        Task<FoodCategory> GetById(int id, bool incluDeleted = false);
        Task<FoodCategory> Edit(int id, FoodCategory foodCategory, bool incluDeleted = false);
        Task Delete(int id, bool incluDeleted = false);
        Task<FoodCategory> GetByFoodItemId(int foodItemId);
    }
}
