using Microsoft.EntityFrameworkCore;
using StoreManagement.Data;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Models;

namespace StoreManagement.Repository
{
    public class AppUserRepository : IAppUserRepository
    {
        private readonly ApplicationDbContext _context;
        public AppUserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<AppUser> CreateAsync(AppUser user)
        {
            
            _context.AppUsers.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task Delete(int id, bool incluDeleted = false)
        {
            var user = _context.AppUsers.Where(obj => obj.Id == id && obj.IsDeleted == incluDeleted).FirstOrDefault();
            _context.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<List<AppUser>> GetAll()
        {
            var list = new List<AppUser>();
            list = await _context.AppUsers.ToListAsync();
            return list.ToList();
        }

        public Task<AppUser> GetById(int id, bool incluDeleted = false)
        {
            var user =  _context.AppUsers.Where(obj => obj.Id == id && obj.IsDeleted == incluDeleted).FirstOrDefaultAsync();
            return user;
        }

        public async Task<List<AppUser>> GetByNameAsync(string name)
        {
            var list = await _context.AppUsers.Where(obj => obj.Name.Contains(name) && obj.IsDeleted == false).ToListAsync();
            return list;
        }

        public async Task<AppUser> UpdateAsync(AppUser appUser, bool incluDeleted = false)
        {
            var updateUser = await _context.AppUsers.FirstOrDefaultAsync(obj => obj.Id == appUser.Id && obj.IsDeleted == incluDeleted);
            if (updateUser != null)
            {
                updateUser.Name = appUser.Name;
                updateUser.Email = appUser.Email;
                updateUser.Address = appUser.Address;
                updateUser.PhoneNumber = appUser.PhoneNumber;
                var role = await _context.Roles.FindAsync(appUser.RoleId);
                if (role != null)
                {
                    updateUser.RoleId = appUser.RoleId;
                }
                else
                {
                    
                }
                await _context.SaveChangesAsync();
            }
            return updateUser;
        }
    }
}
