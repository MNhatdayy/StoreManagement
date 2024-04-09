using StoreManagement.DTO;
using StoreManagement.Models;

namespace StoreManagement.Interfaces.IServices
{
    public interface IAppUserService
    {
        Task<AppUserDTO> CreateAsync(AppUser appUser);
        Task<List<AppUserDTO>> GetAllAsync();
        Task<AppUserDTO> GetByIdAsync(int id, bool incluDeleted = false);
        Task<bool> Delete(int id, bool incluDeleted = false);
        Task<AppUserDTO> UpdateAsync(AppUserDTO appUserDTO, bool incluDeleted = false);
        Task<List<AppUserDTO>> GetByNameAsync(string name);
        Task<AppUserDTO> GetByEmailAsync(string name);

    }
}
