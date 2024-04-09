using StoreManagement.Models;

namespace StoreManagement.DTO
{
    public class OrderDetailDTO
    {
        public int FoodId { get; set; }
        public FoodItemDTO FoodItem { get; set; }
        public int OrderId { get; set; }
        public OrderDTO Order { get; set; }
        public int Quantity { get; set; } = 1;
        public string Notes { get; set; }
        public double Price { get; set; }
    }
}
