using StoreManagement.Models;

namespace StoreManagement.Interfaces.IRepositorys
{
    public interface IAppUserRepository
    {
        Task<AppUser> CreateAsync(AppUser user);
        Task<List<AppUser>> GetAll();
        Task<AppUser> GetById(int id, bool incluDeleted = false);
        Task Delete(int id, bool incluDeleted = false);
        Task<AppUser> UpdateAsync(AppUser appUser, bool incluDeleted = false);
        Task<List<AppUser>> GetByNameAsync(string name);
        Task<AppUser> GetByEmailAsync(string email);
    }
}
