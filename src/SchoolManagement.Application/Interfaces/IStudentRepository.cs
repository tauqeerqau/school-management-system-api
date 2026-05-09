using SchoolManagement.Application.DTOs;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Interfaces
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync(StudentQueryParameters parameters);

        Task<Student?> GetByIdAsync(int id);

        Task<Student> AddAsync(Student student);

        Task UpdateAsync(Student student);

        Task DeleteAsync(Student student);
    }
}
