using StoreManagement.Models;

namespace StoreManagement.Interfaces.IRepositorys
{
    public interface IAppUserRepository
    {
        Task<AppUser> CreateAsync(AppUser user);
        Task<List<AppUser>> GetAll();
        Task<AppUser> GetById(int id, bool incluDeleted = false);
        Task Delete(int id, bool incluDeleted = false);
    }
}
