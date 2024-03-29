namespace StoreManagement.Models
{
    public class Table : DeletableEntity
    {
        public DateTime CreateTime { get; set; } = DateTime.Now;
        public bool Status { get; set; }
        public int StoreID { get; set; }
    }
}
