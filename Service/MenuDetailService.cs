using AutoMapper;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;

namespace StoreManagement.Service
{
    public class MenuDetailService : IMenuDetailService
    {
        private readonly IMenuDetailRepository menuDetailRepository;
        private readonly IMapper _mapper;
        public MenuDetailService(IMenuDetailRepository menuDetailRepository, IMapper mapper)
        {
            this.menuDetailRepository = menuDetailRepository;
            this._mapper = mapper;
        }

        public async Task<bool> AddMenuItem(int menuID, int foodItemId)
        {
            await menuDetailRepository.AddMenuItem(menuID, foodItemId);
            return true;
        }

        public async Task Delete(int menuId, int foodItemId)
        {
            await menuDetailRepository.Delete(menuId,foodItemId);
        }

        public async Task<List<MenuDetailDTO>> GetAll()
        {
            var list = await menuDetailRepository.GetAll();
            return _mapper.Map<List<MenuDetailDTO>>(list);
        }

        public async Task<MenuDetailDTO> GetById(int menuId)
        {
            var menu = await menuDetailRepository.GetById(menuId);
            return _mapper.Map<MenuDetailDTO>(menu);
        }

        public async Task<List<MenuDetailDTO>> GetMenuDetailsByMenuId(int menuId)
        {
            var list = await menuDetailRepository.GetMenuDetailsByMenuId(menuId);
            return _mapper.Map<List<MenuDetailDTO>>(list);
        }

        public async Task<MenuDetailDTO> GetByMenuIdAndFoodItemId(int menuId, int foodItemId)
        {
            var menuDetail = await menuDetailRepository.GetByMenuIdAndFoodItemId(menuId, foodItemId);
            if (menuDetail == null)
            {
                return null; 
            }
            var menuDetailDTO = new MenuDetailDTO
            {
                MenuId = menuDetail.MenuId,
                FoodItemId = menuDetail.FoodItemId,
            };
            return menuDetailDTO;
        }

        public void UpdateMenuStatus(int menuId, int foodItemId, int status)
        {
            menuDetailRepository.UpdateMenuStatus(menuId, foodItemId, status);
        }

    }
}
