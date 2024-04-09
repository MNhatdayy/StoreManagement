
using System.ComponentModel.DataAnnotations;

namespace StoreManagement.Models
{
    public class AppUser : DeletableEntity
    {
        [Required]
        [StringLength(30, MinimumLength = 4, ErrorMessage = "Vui lòng nhập đầy đủ tên")]
        public string Name { get; set; }
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }
        [Required]
        [StringLength(30, ErrorMessage = "Vui lòng không để trống mật khẩu")]
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
