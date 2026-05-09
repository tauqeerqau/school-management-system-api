using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs
{
    public class CreateStudentDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }

        public string Gender { get; set; } = string.Empty;
    }
}
