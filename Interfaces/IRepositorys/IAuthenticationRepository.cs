using StoreManagement.Models;

namespace StoreManagement.Interfaces.IRepositorys
{
    public interface IAuthenticationRepository
    { 
        public Task<AppUser> FindByEmailAsync(string email);
        public Task<bool> ChangePasswordAsync(AppUser user,string currentPassword ,string newPassword);
        public Task<bool> CheckPasswordAsync(AppUser user, string password);
        public Task<bool> CreateAsync(AppUser user, string password);
        public Task UpdateUserAsync(AppUser user);  
        public Task<bool> AddToRoleAsync(AppUser appUser,int id);
    }
}
