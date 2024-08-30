using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Sneaker_Ecommerce.Domain.Enum;

namespace Sneaker_Ecommerce.Domain.Entity
{
    public class User : DeleteableEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(150)")]
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public Role Role {  get; set; }
        public virtual Collection<Order> Orders { get; set; }
    }
}
