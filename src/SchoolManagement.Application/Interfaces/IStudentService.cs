using SchoolManagement.Application.Common;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Interfaces
{
    public interface IStudentService
    {
        Task<ApiResponse<IEnumerable<StudentDto>>> GetAllStudentsAsync(StudentQueryParameters parameters);

        Task<ApiResponse<StudentDto>> GetStudentByIdAsync(int id);

        Task<ApiResponse<StudentDto>> CreateStudentAsync(CreateStudentDto dto);

        Task<ApiResponse<string>> UpdateStudentAsync(int id, UpdateStudentDto dto);

        Task<ApiResponse<string>> DeleteStudentAsync(int id);
    }
}
