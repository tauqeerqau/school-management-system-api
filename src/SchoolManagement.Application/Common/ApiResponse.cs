using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T Data { get; set; }

        public PaginationMetadata? Pagination { get; set; }

        public ApiResponse(
            bool success,
            string message,
            T data,
            PaginationMetadata pagination)
        {
            Success = success;
            Message = message;
            Data = data;
            Pagination = pagination;
        }
    }
}
