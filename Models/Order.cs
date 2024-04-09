using System.Collections.ObjectModel;

namespace StoreManagement.Models
{
    public class Order : DeletableEntity
    {
        public int TableId { get; set; }
        public Table Table { get; set; }
        public int SumFood { get; set; }
        public double Incurred { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual Invoice Invoice { get; set; }
        public bool StatusPay {  get; set; }
        public bool status {  get; set; }

    }
}
