using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneaker_Ecommerce.Domain.Entity
{
    public class Payment : DeleteableEntity
    {
        public int OrderId { get; set; }
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        public DateTime PayemntDate { get; set; }   
        public decimal Amount { get; set; } 
        public string PaymentMethod { get; set; }
    }
}
