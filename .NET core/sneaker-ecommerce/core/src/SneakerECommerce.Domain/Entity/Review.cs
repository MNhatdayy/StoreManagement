using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneaker_Ecommerce.Domain.Entity
{
    public class Review : DeleteableEntity
    {
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
