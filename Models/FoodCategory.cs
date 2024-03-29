namespace StoreManagement.Models
{
    public class FoodCategory : DeletableEntity
    {
        public string Name { get; set; }
        public List<FoodItem> FoodItems { get; set; }
    }
}
