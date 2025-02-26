﻿using InternManagement.Models;

namespace InternManagement.DTOs.Responses
{
    public class UserResponse
    {
        public int? UserId { get; set; }
        public string? FullName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public int? RoleId { get; set; }
    }
}
