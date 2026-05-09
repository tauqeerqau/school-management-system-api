using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.DTOs
{
    public class PaginationMetadata
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public int TotalRecords { get; set; }

        public int TotalPages { get; set; }
    }
}
