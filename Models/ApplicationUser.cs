﻿using Microsoft.AspNetCore.Identity;

namespace FireStockAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; } = string.Empty;
    }
}
