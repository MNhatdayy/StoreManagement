namespace StoreManagement.DTO
{
    public class StoreDTO
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string AddressStore { get; set; }
        public int UserId { get; set; }
        public AppUserDTO User { get; set; }
    }
}
