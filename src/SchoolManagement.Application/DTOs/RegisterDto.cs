using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs
{
    public class RegisterDto
    {
        public string Username { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
