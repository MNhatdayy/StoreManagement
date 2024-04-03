using Microsoft.EntityFrameworkCore;
using StoreManagement.Data;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Models;

namespace StoreManagement.Repository
{
    public class MenuDetailRepository : IMenuDetailRepository
    {
        readonly ApplicationDbContext _context;
        public MenuDetailRepository(ApplicationDbContext context) 
        {
            _context = context;
        }
        public async Task AddMenuItem(int menuId, int foodItemId)
        {
            try
            {
                if (menuId <= 0 || foodItemId <= 0)
                {
                    throw new ArgumentException("Invalid food and menu item ID");
                }
                var menudetail = new MenuDetail
                {
                    MenuId = menuId,
                    FoodItemId = foodItemId,
                };
                _context.MenuDetails.Add(menudetail);
                await _context.SaveChangesAsync();
            }catch (Exception ex)
            {
                var innerException = ex.InnerException;
                Console.WriteLine($"Inner Exception: {innerException.Message}");
            }
        }

        public async Task Delete(int menuId, int foodItemId)
        {
            var menuDetail = await _context.MenuDetails.FirstOrDefaultAsync(md => md.MenuId == menuId && md.FoodItemId == foodItemId);
            if (menuDetail != null)
            {
                _context.Remove(menuDetail);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<MenuDetail>> GetAll()
        {
            return await _context.MenuDetails
                .Include("FoodItems")
                .Include("FoodItems.FoodCategory")
                .ToListAsync();
        }

        public  Task<MenuDetail> GetById(int menuId)
        {
            var menuDetail =  _context.MenuDetails.FirstOrDefaultAsync(obj => obj.MenuId == menuId); ;
            return menuDetail;
        }

        public async Task<List<MenuDetail>> GetMenuDetailsByMenuId(int menuId)
        {
            return await _context.MenuDetails
                .Where(md => md.MenuId == menuId)
                .Include("FoodItems.FoodCategory")
                .Include("Menu")
                .ToListAsync();
        }

        public async Task<MenuDetail> GetByMenuIdAndFoodItemId(int menuId, int foodItemId)
        {
            return await _context.MenuDetails
                .FirstOrDefaultAsync(md => md.MenuId == menuId && md.FoodItemId == foodItemId);
        }

    }
}
