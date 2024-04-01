using System.Collections.ObjectModel;

namespace StoreManagement.Models
{
    public class FoodItem : DeletableEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public int FoodCategoryId { get; set; }
        public virtual FoodCategory FoodCategory { get; set; }
        public virtual ICollection<MenuDetail> MenuDetails { get; set; }
    }
}
