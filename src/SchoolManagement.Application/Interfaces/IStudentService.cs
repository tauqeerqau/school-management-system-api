using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Interfaces
{
    public interface IStudentService
    {
        Task<List<StudentDto>> GetAllAsync();
    }
}
