using StoreManagement.Models;

namespace StoreManagement.DTO
{
    public class OrderDetailsDTO
    {
        public int Id {  get; set; } 
        public int IdFood { get; set; }
        public decimal TotalPrice { get; set; }
        public int SumFood { get; set; }
        public string OderredDish { get; set; }
        public OrderDTO Order { get; set; }
    }
}
