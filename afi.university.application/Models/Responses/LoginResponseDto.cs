﻿
using afi.university.domain.Common.Enums;

namespace afi.university.application.Models.Responses
{
    public class LoginResponseDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public UserRole Role { get; set; }
        public string? Token { get; set; }
    }
}
