using System.Collections.ObjectModel;

namespace StoreManagement.Models
{
    public class OrderDetail 
    {
        public int FoodId { get; set; }
        public FoodItem FoodItem { get; set; }
        public int OrderId {  get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public string Notes { get; set; }
        public double Price { get; set; }
    }
}
