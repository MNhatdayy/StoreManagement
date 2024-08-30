using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneaker_Ecommerce.Domain.Entity
{
    public class Shipment : DeleteableEntity
    {
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public string TrackingNumber { get; set; }
        public DateTime ShipmentDate { get; set; }
        public string Carrier {  get; set; }
        public string Status { get; set; }
    }
}
