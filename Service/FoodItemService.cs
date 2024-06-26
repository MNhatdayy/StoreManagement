﻿using AutoMapper;
using StoreManagement.DTO;
using StoreManagement.Interfaces.IRepositorys;
using StoreManagement.Interfaces.IServices;
using StoreManagement.Models;

namespace StoreManagement.Service
{
    public class FoodItemService : IFoodItemService
    {
        public readonly IFoodItemRepository foodItemRepository;
        public readonly IMapper _mapper;

        public FoodItemService(IFoodItemRepository foodItemRepository, IMapper mapper)
        {
            this.foodItemRepository = foodItemRepository;
            _mapper = mapper;
        }

        public async Task<FoodItemDTO> Create(FoodItemDTO foodItemDTO)
        {
            var foodItem = _mapper.Map<FoodItem>(foodItemDTO);

            var result = await foodItemRepository.Create(foodItem);
            return _mapper.Map<FoodItemDTO>(result);
        }

        public async Task<bool> Delete(int id, bool incluDeleted = false)
        {
            await foodItemRepository.Delete(id, incluDeleted);
            return true;
        }

        public async Task<FoodItemDTO> Edit(int id, FoodItemDTO foodItemDTO,IFormFile uFile, bool incluDeleted = false)
        {
            var foodItem = await foodItemRepository.GetById(id);
            if (foodItem == null)
            {
                return null;
            }
            foodItem.Name = foodItemDTO.Name;
            foodItem.Price = foodItemDTO.Price;
            foodItem.Description = foodItemDTO.Description;
            foodItem.ImageUrl = foodItemDTO.ImageUrl;
            foodItem.FoodCategoryId = foodItemDTO.FoodCategoryId;
            await foodItemRepository.Edit(id, foodItem,uFile);
            return _mapper.Map<FoodItemDTO>(foodItem);
        }

        public async Task<List<FoodItemDTO>> GetAll(List<int> categoriesId)
        {
            var list = await foodItemRepository.GetAll(categoriesId);
            return _mapper.Map<List<FoodItemDTO>>(list);
        }

        public async Task<FoodItemDTO> GetById(int id, bool incluDeleted = false)
        {
            var foodItem = await foodItemRepository.GetById(id, incluDeleted);
            return _mapper.Map<FoodItemDTO>(foodItem);
        }

        public async Task<int?> GetStoreIdByFoodItemId(int foodItemId)
        {
            var foodItem = await foodItemRepository.GetById(foodItemId);
            if (foodItem != null)
            {
                return foodItem.FoodCategory?.StoreId;
            }
            return null;
        }

        public async Task<string> SaveImages(IFormFile uFile)
        {
            var urlfile = await foodItemRepository.SaveImage( uFile);
            return urlfile;
        }
    }
}
