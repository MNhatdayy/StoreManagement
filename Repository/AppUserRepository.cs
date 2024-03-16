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
            var newUser = new AppUser()
            {
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                PhoneNumber = user.PhoneNumber,
                RoleId = user.RoleId,
                Password = user.Password,
                IsDeleted = false
            };
            _context.Add(newUser);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task Delete(int id, bool incluDeleted = false)
        {
            var user = _context.AppUsers.Where(obj => obj.Id == id && incluDeleted).FirstOrDefault();
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
            var user =  _context.AppUsers.Where(obj => obj.Id == id && incluDeleted).FirstOrDefaultAsync();
            return user;
        }
    }
}
