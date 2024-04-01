namespace StoreManagement.Models
{
    public class Role : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
