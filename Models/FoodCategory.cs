using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace StoreManagement.Models
{
    public class FoodCategory : DeletableEntity
    {
        public string Name { get; set; }
        public ICollection<FoodItem> FoodItems { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

    }
}
