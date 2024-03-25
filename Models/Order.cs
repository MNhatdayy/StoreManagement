using System.Collections.ObjectModel;

namespace StoreManagement.Models
{
    public class Order : DeletableEntity
    {
        public int TableId { get; set; }
        public int IdStore { get; set; }
        public Store Store { get; set; }
        public int IdPayment { get; set; }
        public decimal Incurred { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime PayTime { get; set; } = DateTime.Now;
        public Collection<OrderDetails> OrderDetails { get; set; }
    }
}
