using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StoreManagement.Data;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Models;

namespace StoreManagement.Repository
{
    public class MenuRepository : IMenuRepository
    {
        private readonly ApplicationDbContext _context;
        public MenuRepository(ApplicationDbContext context) 
        {
            _context = context;
        }

        public async Task<Menu> Create(Menu menu)
        {
            var newMenu = new Menu()
            {
                StoreId = menu.StoreId,
                CreateTime = menu.CreateTime = DateTime.Now,
            };
            _context.Menus.Add(newMenu);
            await _context.SaveChangesAsync();
            return newMenu;
        }

        public async Task Delete(int id, bool incluDeleted = false)
        {
            var menu = _context.Menus.Where(obj => obj.Id == id && obj.IsDeleted == incluDeleted).FirstOrDefault();
            if (menu != null)
            {
                menu.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Menu> Edit(int id, Menu Menu, bool incluDeleted = false)
        {
            var menus = await _context.Menus.FindAsync(id);
            if (menus == null)
            {
                return null;
            }
            menus.Id = id;
            menus.StoreId = Menu.StoreId;
            await _context.SaveChangesAsync();
            return menus;
        }

        public async Task<List<Menu>> GetAll(bool incluDeleted = false)
        {
            if (!incluDeleted)
            {
                return await _context.Menus.Where(m => !m.IsDeleted).Include("Store").ToListAsync();
            }
            else
            {
                return await _context.Menus.ToListAsync();
            }
        }

        public async Task<Menu> GetMenuByIdStore(int idStore, bool includeDeleted = false)
        {
                var menu = await _context.Menus.Where(obj => obj.StoreId == idStore && obj.IsDeleted == includeDeleted).FirstOrDefaultAsync();
                return menu;
        }

        public Task<Menu> GetById(int id, bool incluDeleted = false)
        {
            var menu = _context.Menus.Where(obj => obj.Id == id && obj.IsDeleted == incluDeleted).FirstOrDefaultAsync();
            return menu;
        }
    }
}
