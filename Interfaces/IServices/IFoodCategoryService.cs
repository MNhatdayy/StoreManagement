﻿using StoreManagement.DTO;
using StoreManagement.Models;
using StoreManagement.Repository;

namespace StoreManagement.Interfaces.IServices
{
    public interface IFoodCategoryService
    {
        Task<FoodCategoryDTO> Create(FoodCategoryDTO foodCategoryDTO);
        Task<List<FoodCategoryDTO>> GetAll();
        Task<FoodCategoryDTO> GetById(int id, bool incluDeleted = false);
        Task<FoodCategoryDTO> Edit(int id, FoodCategoryDTO foodCategoryDTO, bool incluDeleted = false);
        Task<bool> Delete(int id, bool incluDeleted = false);

    }
}
