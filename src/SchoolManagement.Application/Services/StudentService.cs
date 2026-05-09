using AutoMapper;
using SchoolManagement.Application.Common;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentService(
            IStudentRepository studentRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<StudentDto>>> GetAllStudentsAsync()
        {
            var students = await _studentRepository.GetAllAsync();

            var studentDtos = _mapper.Map<IEnumerable<StudentDto>>(students);

            return new ApiResponse<IEnumerable<StudentDto>>
            {
                Success = true,
                Message = "Students fetched successfully",
                Data = studentDtos
            };
        }

        public async Task<ApiResponse<StudentDto>> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return new ApiResponse<StudentDto>
                {
                    Success = false,
                    Message = "Student not found"
                };
            }

            var studentDto = _mapper.Map<StudentDto>(student);

            return new ApiResponse<StudentDto>
            {
                Success = true,
                Message = "Student fetched successfully",
                Data = studentDto
            };
        }

        public async Task<ApiResponse<StudentDto>> CreateStudentAsync(CreateStudentDto dto)
        {
            var student = _mapper.Map<Student>(dto);

            await _studentRepository.AddAsync(student);

            var studentDto = _mapper.Map<StudentDto>(student);

            return new ApiResponse<StudentDto>
            {
                Success = true,
                Message = "Student created successfully",
                Data = studentDto
            };
        }

        public async Task<ApiResponse<string>> UpdateStudentAsync(int id, UpdateStudentDto dto)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(id);

            if (existingStudent == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Student not found"
                };
            }

            _mapper.Map(dto, existingStudent);

            await _studentRepository.UpdateAsync(existingStudent);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Student updated successfully",
                Data = null
            };
        }

        public async Task<ApiResponse<string>> DeleteStudentAsync(int id)
        {
            var existingStudent = await _studentRepository.GetByIdAsync(id);

            if (existingStudent == null)
            {
                return new ApiResponse<string>
                {
                    Success = false,
                    Message = "Student not found"
                };
            }

            await _studentRepository.DeleteAsync(existingStudent);

            return new ApiResponse<string>
            {
                Success = true,
                Message = "Student deleted successfully",
                Data = null
            };
        }
    }
}
