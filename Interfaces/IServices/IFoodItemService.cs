using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using StoreManagement.DTO;

namespace StoreManagement.Interfaces.IServices
{
    public interface IFoodItemService
    {
        Task<FoodItemDTO> Create(FoodItemDTO foodItemDTO);
        Task<List<FoodItemDTO>> GetAll();
        Task<FoodItemDTO> GetById(int id, bool incluDeleted = false);
        Task<FoodItemDTO> Edit(int id, FoodItemDTO foodItemDTO, IFormFile uFile, bool incluDeleted = false);
        Task<bool> Delete(int id, bool incluDeleted = false);
        Task<string> SaveImages(string url, IFormFile uFile);

    }
}
