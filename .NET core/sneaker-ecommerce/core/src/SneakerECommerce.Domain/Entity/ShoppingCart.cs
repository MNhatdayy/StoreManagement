using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneaker_Ecommerce.Domain.Entity
{
    public class ShoppingCart : DeleteableEntity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Collection<CartItem> CartItems { get; set; }
    }
}
