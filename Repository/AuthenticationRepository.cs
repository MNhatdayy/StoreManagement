using Microsoft.EntityFrameworkCore;
using StoreManagement.Data;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Models;

namespace StoreManagement.Repository
{
    public class AuthenticationRepository : IAuthenticationRepository
    {
        private readonly ApplicationDbContext _context;

        public AuthenticationRepository(ApplicationDbContext context)
        {
            _context = context;

        }

        public async Task<bool> AddToRoleAsync(AppUser appUser, int id)
        {
            var newUser = new AppUser
            {
                Id = appUser.Id,
                Name = appUser.Name,
                Email = appUser.Email,
                Password = appUser.Password,
                RoleId = id
            };
            var user = await _context.AppUsers.Where(obj => obj.Id == newUser.Id).FirstOrDefaultAsync();
            _context.AppUsers.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ChangePasswordAsync(AppUser user, string currentPassword, string newPassword)
        {
            if(user == null || user.Id <=0 || !user.IsDeleted)
            {
                return false;
            }
            var userChangePass = await _context.AppUsers.Where(obj=>obj.Id == user.Id).FirstOrDefaultAsync();
            if(currentPassword == null || newPassword == null)
            {
                return false;
            }
            userChangePass.Password = newPassword;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> CheckPasswordAsync(AppUser user, string password)
        {
            if(user.Password.Equals(password))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> CreateAsync(AppUser user, string password)
        {
            var newUser = new AppUser
            {
                Email = user.Email,
                Password = password,
                Name = user.Name,

            };
            await _context.AppUsers.AddAsync(newUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<AppUser> FindByEmailAsync(string email)
        {
            return await _context.AppUsers.FirstOrDefaultAsync(x => x.Email.Equals(email)); 
        }

        public async Task UpdateUserAsync(AppUser user)
        {
            _context.AppUsers.Update(user);
            await _context.SaveChangesAsync();
            
        }
    }
}
