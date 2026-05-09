using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<List<StudentDto>> GetAllAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            return students.Select(student => new StudentDto
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                Email = student.Email,
                DateOfBirth = student.DateOfBirth,
                Gender = student.Gender
            }).ToList();
        }
    }
}
