using StoreManagement.Models;

namespace StoreManagement.Interfaces.IRepositorys
{
    public interface IFoodCategoryRepository
    {
        Task<FoodCategory> Create(FoodCategory foodCategory);
        Task<List<FoodCategory>> GetAll(List<int> StoreId, bool incluDeleted = false);
        Task<FoodCategory> GetById(int id, bool incluDeleted = false);
        List<FoodCategory> GetByStoreId(int id, bool incluDeleted = false);
        Task<FoodCategory> Edit(int id, FoodCategory foodCategory, bool incluDeleted = false);
        Task Delete(int id, bool incluDeleted = false);
    }
}
