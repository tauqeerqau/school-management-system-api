using SchoolManagement.Application.DTOs;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Interfaces
{
    public interface IStudentRepository
    : IGenericRepository<Student>
    {
        Task<List<Student>> GetAllAsync(StudentQueryParameters parameters);
    }
}
