using System.Collections.ObjectModel;

namespace StoreManagement.Models
{
    public class Order : DeletableEntity
    {
        public int TableId { get; set; }
        public Table Table { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public int InvoiceId { get; set; }
        public int SumFood { get; set; }
        public decimal Incurred { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public Collection<OrderDetail> OrderDetails { get; set; }
    }
}
