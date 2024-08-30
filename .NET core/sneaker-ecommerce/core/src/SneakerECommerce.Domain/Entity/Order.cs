using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneaker_Ecommerce.Domain.Entity
{
    public class Order : DeleteableEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public string status { get; set; }
        public string ShippingAddress { get; set; }
        public virtual Collection<OrderItem> OrderItems {  get; set; }
        public virtual Payment Payment { get; set; }
        public virtual Shipment Shipment { get; set; }
    }
}
