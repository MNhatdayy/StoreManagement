using StoreManagement.Models;
using System.Collections.ObjectModel;

namespace StoreManagement.DTO
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public TableDTO Table { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime PayTime { get; set; } = DateTime.Now;
        public ICollection<OrderDetailsDTO> OrderDetails { get; set; }
        public virtual ICollection<InvoiceDTO> Invoice { get; set; }
        public bool status { get; set; }
        public double TotalPrice { get; set; }
        public bool StatusPay { get; set; }
    }
}
