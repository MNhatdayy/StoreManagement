namespace StoreManagement.DTO
{
    public class TableDTO
    {
        public int Id { get; set; }
        public DateTime CreateTime { get; set; }
        public bool Status { get; set; }
        public int StoreID { get; set; }
        public bool IsDeleted { get; set; }
    }
}
