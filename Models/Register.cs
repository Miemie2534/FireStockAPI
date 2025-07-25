﻿namespace FireStockAPI.Models
{
    public class Register
    {
        public int Id { get; set; }
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string role { get; set; } = "User";
    }
}
