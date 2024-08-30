using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneaker_Ecommerce.Domain.Entity
{
    public class CartItem : DeleteableEntity
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int CardId { get; set; }
        [ForeignKey("CardId")]
        public virtual ShoppingCart ShoppingCart { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

    }
}
