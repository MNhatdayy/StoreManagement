using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sneaker_Ecommerce.Domain.Entity
{
    public class Category : DeleteableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual Collection<Product> Products { get; set; }
    }
}
