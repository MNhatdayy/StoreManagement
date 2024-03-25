using StoreManagement.DTO;

namespace StoreManagement.Interfaces.IServices
{
    public interface IStoreService
    {
        Task<StoreDTO> CreateAsync(StoreDTO storeDto);
        Task<List<StoreDTO>> GetAllAsync();
        Task<StoreDTO> GetByIdAsync(int id, bool incluDeleted = false);
        Task<bool> Delete(int id, bool incluDeleted = false);
        Task<StoreDTO> UpdateAsync(StoreDTO storeDto, bool incluDeleted = false);
        Task<IList<StoreDTO>> GetByNameAsync(string name, bool incluDeleted = false);
    }
}
