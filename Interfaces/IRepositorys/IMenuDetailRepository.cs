using Microsoft.EntityFrameworkCore;
using StoreManagement.DTO;
using StoreManagement.Models;

namespace StoreManagement.Interfaces.IRepositorys
{
    public interface IMenuDetailRepository
    {
        Task<List<MenuDetail>> GetMenuDetailsByMenuId(int menuId);
        Task<MenuDetail> GetByMenuIdAndFoodItemId(int menuId, int foodItemId);
        Task AddMenuItem(int menuId,int foodItemId);
        Task<List<MenuDetail>> GetAll();
        Task<MenuDetail> GetById(int menuId);
        Task Delete(int menuId, int foodItemId);
        void UpdateMenuStatus(int menuId, int foodItemId, int status);

    }
}
