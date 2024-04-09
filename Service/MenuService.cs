using AutoMapper;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;
using StoreManagement.Repository;

namespace StoreManagement.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository menuRepository;
        private readonly IMapper _mapper;
        public MenuService(IMenuRepository menuRepository, IMapper mapper) 
        {
            this.menuRepository = menuRepository;
            this._mapper = mapper;
        }

        public async Task<MenuDTO> Create(MenuDTO menuDTO)
        {
            var menu = _mapper.Map<Menu>(menuDTO);

            var result = await menuRepository.Create(menu);
            return _mapper.Map<MenuDTO>(result);
        }

        public async Task<bool> Delete(int id, bool incluDeleted = false)
        {
            await menuRepository.Delete(id, incluDeleted);
            return true;
        }

        public async Task<MenuDTO> Edit(int id, MenuDTO menuDTO, bool incluDeleted = false)
        {
            var menu = await menuRepository.Edit(id, _mapper.Map<Menu>(menuDTO));
            if (menu == null)
            {
                return null;
            }
            return _mapper.Map<MenuDTO>(menu);
        }

        public async Task<List<MenuDTO>> GetAll()
        {
            var list = await menuRepository.GetAll();
            return _mapper.Map<List<MenuDTO>>(list);
        }

        public async Task<MenuDTO> GetMenuByIdStore(int idStore, bool incluDeleted = false)
        {
            var menu = await menuRepository.GetMenuByIdStore(idStore,incluDeleted);
            return _mapper.Map<MenuDTO>(menu);
        }

        public async Task<MenuDTO> GetById(int id, bool incluDeleted = false)
        {
            var menu = await menuRepository.GetById(id, incluDeleted);
            return _mapper.Map<MenuDTO>(menu);
        }
    }
}
