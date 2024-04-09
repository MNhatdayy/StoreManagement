using StoreManagement.Models;

namespace StoreManagement.Interfaces.IRepositorys
{
    public interface ITableRepository
    {
        Task<Table> Create(Table table);
        Task<List<Table>> GetAll(bool incluDeleted = false);
        Task<Table> GetById(int id, bool incluDeleted = false);
        Task<Table> Edit(int id ,Table table , bool incluDeleted = false);
        Task Delete(int id, bool incluDeleted = false);
        Task<List<Table>> GetAllByStoreId(int id);
    }
}
