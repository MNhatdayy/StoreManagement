﻿namespace StoreManagement.DTO.AuthDTO
{
    public class ChangePasswordDTO
    {
        public string Email { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
