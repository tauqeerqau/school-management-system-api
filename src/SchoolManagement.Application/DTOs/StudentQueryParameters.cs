using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs
{
    public class StudentQueryParameters
    {
        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 10;

        public string? Search { get; set; }

        public string? Gender { get; set; }
    }
}
