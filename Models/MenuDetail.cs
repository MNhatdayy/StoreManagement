namespace StoreManagement.Models
{
    public class MenuDetail
    {
        public int Status { get; set; } = 1;
        public DateTime UpdateTime { get; set; } = DateTime.Now;    
        public int MenuId { get; set; }
        public virtual Menu Menu { get; set; }
        public int FoodItemId { get; set; }
        public virtual FoodItem FoodItem { get; set; }
    }
}
