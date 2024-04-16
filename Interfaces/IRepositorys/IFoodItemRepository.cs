using StoreManagement.DTO;
using StoreManagement.Models;

namespace StoreManagement.Interfaces.IRepositorys
{
    public interface IFoodItemRepository
    {
        Task<FoodItem> Create(FoodItem foodItem);
        Task<List<FoodItem>> GetAll(List<int> categoryId, bool incluDeleted = false);
        Task<FoodItem> GetById(int id, bool incluDeleted = false);
        Task<FoodItem> Edit(int id, FoodItem foodItem, IFormFile uFile, bool incluDeleted = false);
        Task Delete(int id, bool incluDeleted = false);
        Task<string> SaveImage(IFormFile uFile);
    }
}
