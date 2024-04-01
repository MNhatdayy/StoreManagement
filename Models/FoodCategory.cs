using System.Collections.ObjectModel;

namespace StoreManagement.Models
{
    public class FoodCategory : DeletableEntity
    {
        public string Name { get; set; }
        public Collection<FoodItem> FoodItems { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }

    }
}
