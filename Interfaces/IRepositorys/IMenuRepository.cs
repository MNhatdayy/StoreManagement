using StoreManagement.Models;

namespace StoreManagement.Interfaces.IRepositorys
{
    public interface IMenuRepository
    {
        Task<Menu> Create(Menu menu);
        Task<List<Menu>> GetAll(List<int> StoreId, bool incluDeleted = false);
        Task<Menu> GetById(int id, bool incluDeleted = false);
        Task<Menu> Edit(int id, Menu Menu, bool incluDeleted = false);
        Task Delete(int id, bool incluDeleted = false);
        Task<Menu> GetMenuByIdStore(int idStore, bool includeDeleted = false);

    }
}
