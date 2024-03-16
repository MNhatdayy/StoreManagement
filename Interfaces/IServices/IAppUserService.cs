using StoreManagement.DTO;

namespace StoreManagement.Interfaces.IServices
{
    public interface IAppUserService
    {
        Task<AppUserDTO> CreateAsync(AppUserDTO appUserDTO);
        Task<List<AppUserDTO>> GetAllAsync();
        Task<AppUserDTO> GetByIdAsync(int id, bool incluDeleted = false);
        Task<bool> Delete(int id, bool incluDeleted = false);
    }
}
