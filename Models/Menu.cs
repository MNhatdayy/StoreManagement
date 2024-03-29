using System.Collections.ObjectModel;

namespace StoreManagement.Models
{
    public class Menu : DeletableEntity
    {
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public int StoreId { get; set; }
        public virtual Collection<MenuDetail> MenuDetails { get; set; }
    }
}
