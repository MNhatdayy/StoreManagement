using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.Models
{
    public class Menu : DeletableEntity
    {
        public DateTime CreateTime { get; set; } = DateTime.Now;

        public int StoreId { get; set; }
        public virtual Store Store { get; set; }
        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
    }
}
