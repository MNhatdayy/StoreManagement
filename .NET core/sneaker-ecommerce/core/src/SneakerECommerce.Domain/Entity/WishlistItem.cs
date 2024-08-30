using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneaker_Ecommerce.Domain.Entity
{
    public class WishlistItem : DeleteableEntity
    {
        public int WishlistId { get; set; }
        [ForeignKey("WishlistId")]
        public virtual Wishlist Wishlist { get; set; }
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
    }
}
