using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneaker_Ecommerce.Domain.Entity
{
    public class Wishlist : DeleteableEntity
    {
        public int UserId { get; set; }
        [ForeignKey ("UserId")]
        public virtual User User { get; set; }
        public DateTime DateCreated { get; set; }
        public virtual Collection<WishlistItem> WishlistItems { get; set; }
    }
}
