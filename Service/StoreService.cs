using AutoMapper;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;

namespace StoreManagement.Service
{
    public class StoreService : IStoreService
    {
        private readonly IStoreRepository _storeRepo;
        private readonly IMapper _mapper;
        private int userId;


        public StoreService(IStoreRepository storeRepo, IMapper mapper)
        {

            _storeRepo = storeRepo;
            _mapper = mapper;
        }
        public async Task<StoreDTO> CreateAsync(StoreDTO storeDto)
        {
            var store = _mapper.Map<Store>(storeDto);
            await _storeRepo.CreateAsync(store);
            var storeResult = _mapper.Map<StoreDTO>(store);
            return storeResult;
        }

        public async Task<bool> Delete(int id, bool incluDeleted = false)
        {
            await _storeRepo.Delete(id, incluDeleted);
            return true;
        }

        public async Task<List<StoreDTO>> GetAllAsync()
        {
            var list = await _storeRepo.GetAll();
            return _mapper.Map<List<StoreDTO>>(list);
        }

        public async Task<StoreDTO> GetByIdAsync(int id, bool incluDeleted = false)
        {
            var store = await _storeRepo.GetByIdAsync(id, incluDeleted);
            return _mapper.Map<StoreDTO>(store);
        }

        public async Task<IList<StoreDTO>> GetByNameAsync(string name, bool incluDeleted = false)
        {
            var list = await _storeRepo.GetByNameAsync(name, incluDeleted);
            return _mapper.Map<List<StoreDTO>>(list);
        }

        public async Task<List<StoreDTO>> GetStoresByUserId(int userId)
        {
            var list = await _storeRepo.GetStoreByUserId(userId);
            return _mapper.Map<List<StoreDTO>>(list);
        }

        public async Task<StoreDTO> UpdateAsync(StoreDTO storeDto, bool incluDeleted = false)
        {
            var store = _mapper.Map<Store>(storeDto);
            await _storeRepo.UpdateAsync(store, incluDeleted);
            return _mapper.Map<StoreDTO>(store);
        }
    }
}
