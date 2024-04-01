using StoreManagement.Models;
using System.Collections.ObjectModel;

namespace StoreManagement.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public Store IdStore { get; set; }
        public int IdPayment { get; set; }
        public decimal Incurred { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime PayTime { get; set; } = DateTime.Now;
        public Collection<OrderDetailsDTO> OrderDetails { get; set; }
    }
}
