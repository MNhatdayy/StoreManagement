using StoreManagement.DTO;

namespace StoreManagement.Interfaces.IServices
{
    public interface IMenuService
    {
        Task<MenuDTO> Create(MenuDTO menuDTO);
        Task<List<MenuDTO>> GetAll();
        Task<MenuDTO> GetById(int id, bool incluDeleted = false);
        Task<MenuDTO> Edit(int id, MenuDTO menuDTO, bool incluDeleted = false);
        Task<bool> Delete(int id, bool incluDeleted = false);
        Task<MenuDTO> GetMenuByIdStore(int idStore, bool incluDeleted = false);
    }
}
