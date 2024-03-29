using AutoMapper;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;
using StoreManagement.Repository;

namespace StoreManagement.Service
{
    public class FoodCategoryService : IFoodCategoryService
    {
        public readonly IFoodCategoryRepository foodCategoryRepository;
        public readonly IMapper _mapper;
        public FoodCategoryService(IFoodCategoryRepository foodCategoryRepository, IMapper _mapper) 
        {
            this.foodCategoryRepository = foodCategoryRepository;
            this._mapper = _mapper;
        }

        public async Task<FoodCategoryDTO> Create(FoodCategoryDTO foodCategoryDTO)
        {
            var foodCategory = _mapper.Map<FoodCategory>(foodCategoryDTO);
            var createdFoodCategory = await foodCategoryRepository.Create(foodCategory);
            return _mapper.Map<FoodCategoryDTO>(createdFoodCategory);
        }

        public async Task<bool> Delete(int id, bool incluDeleted = false)
        {
            await foodCategoryRepository.Delete(id, incluDeleted);
            return true;
        }

        public async Task<FoodCategoryDTO> Edit(int id, FoodCategoryDTO foodCategoryDTO, bool incluDeleted = false)
        {
            var foodcategory = await foodCategoryRepository.Edit(id, _mapper.Map<FoodCategory>(foodCategoryDTO));
            if (foodcategory == null)
            {
                return null;
            }
            return _mapper.Map<FoodCategoryDTO>(foodcategory);
        }

        public async Task<List<FoodCategoryDTO>> GetAll()
        {
            var list = await foodCategoryRepository.GetAll();
            return _mapper.Map<List<FoodCategoryDTO>>(list);
        }

        public async Task<FoodCategoryDTO> GetById(int id, bool incluDeleted = false)
        {
            var foodCategory = await foodCategoryRepository.GetById(id, incluDeleted);
            return _mapper.Map<FoodCategoryDTO>(foodCategory);
        }
    }
}
