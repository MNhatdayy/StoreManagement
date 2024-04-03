namespace StoreManagement.DTO
{
    public class MenuDTO
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; } 
        public int StoreId { get; set; }
        public StoreDTO Store { get; set; }
    }
}
