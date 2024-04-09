namespace StoreManagement.DTO
{
    public class FoodCategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StoreId {  get; set; }
        public StoreDTO Store { get; set; }
    }
}
