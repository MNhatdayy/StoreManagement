using StoreManagement.Models;

namespace StoreManagement.Interfaces.IRepositorys
{
    public interface IStoreRepository
    {
        Task<Store> CreateAsync(Store store);
        Task<IList<Store>> GetAll();
        Task<Store> GetByIdAsync(int id, bool incluDeleted = false);
        Task Delete( int id, bool incluDeleted = false);
        Task<IList<Store>> GetByNameAsync(string name, bool incluDeleted = false);
        Task<Store> UpdateAsync(Store store, bool incluDeleted = false);

        Task<List<Store>> GetStoreByUserId(int userId);
    }
}
