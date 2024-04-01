using StoreManagement.Models;

namespace StoreManagement.DTO
{
    public class MenuDetailDTO
    {
        public int Id { get; set; }
        public DateTime UpdateTime { get; set; }
        public int Status { get; set; } = 1;
        public int MenuId { get; set; }
        public int FoodItemId { get; set; }
    }
}
