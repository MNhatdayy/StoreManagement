﻿namespace StoreManagement.DTO
{
    public class AppUserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }

        public bool IsDeleted { get; set; }
        public int RoleId { get; set; }
    }
}
