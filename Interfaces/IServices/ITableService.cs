using StoreManagement.DTO;

namespace StoreManagement.Interfaces.IServices
{
    public interface ITableService
    {
        Task<TableDTO> Create(TableDTO tableDTO);
        Task<List<TableDTO>> GetAll();
        Task<TableDTO> GetById(int id, bool incluDeleted = false);
        Task<TableDTO> Edit(int id, TableDTO tableDTO, bool incluDeleted = false);
        Task<bool> Delete(int id, bool incluDeleted = false);
    }
}
