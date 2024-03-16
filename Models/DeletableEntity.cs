namespace StoreManagement.Models
{
    public class DeletableEntity : BaseEntity
    {
        public bool IsDeleted { get; set; }
    }
}
