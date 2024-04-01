using System.Collections.ObjectModel;

namespace StoreManagement.Models
{
    public class OrderDetail 
    {
        public int FoodId { get; set; }
        public FoodItem FoodItem { get; set; }
        public int OderId {  get; set; }
        public Order Order { get; set; }

    }
}
