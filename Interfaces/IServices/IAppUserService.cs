using StoreManagement.DTO;


namespace StoreManagement.Interfaces.IServices
{
    public interface IAppUserService
    {
        Task<AppUserDTO> CreateAsync(AppUserDTO appUserDTO);
        Task<List<AppUserDTO>> GetAllAsync();
        Task<AppUserDTO> GetByIdAsync(int id, bool incluDeleted = false);
        Task<bool> Delete(int id, bool incluDeleted = false);
        Task<AppUserDTO> UpdateAsync(AppUserDTO appUserDTO, bool incluDeleted = false);
        Task<List<AppUserDTO>> GetByNameAsync(string name);
    }
}
