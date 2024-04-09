using StoreManagement.Models;

namespace StoreManagement.DTO
{
    public class FoodItemDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public int FoodCategoryId { get; set; }
        public FoodCategoryDTO FoodCategory { get; set; }
        
    }
}
