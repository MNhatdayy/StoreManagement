using StoreManagement.DTO;
using StoreManagement.Models;

namespace StoreManagement.Interfaces.IServices
{
    public interface IMenuDetailService
    {
        Task<List<MenuDetailDTO>> GetMenuDetailsByMenuId(int menuId);
        Task<MenuDetailDTO> GetByMenuIdAndFoodItemId(int menuId, int foodItemId);
        Task<bool> AddMenuItem(int menuId, int foodItemId);
        Task<List<MenuDetailDTO>> GetAll();
        Task<MenuDetailDTO> GetById(int menuId);
        Task Delete(int menuId, int foodItemId);
    }
}
