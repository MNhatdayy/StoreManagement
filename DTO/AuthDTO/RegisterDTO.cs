namespace StoreManagement.DTO.AuthDTO
{
    public class RegisterDTO : LoginDTO
    {
        public string FullName { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
