using AutoMapper;
using SchoolManagement.Application.Common;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;

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

        public async Task<ApiResponse<IEnumerable<StudentDto>>> GetAllStudentsAsync(
            StudentQueryParameters parameters)
        {
            var students = await _studentRepository.GetAllAsync(parameters);
            var totalRecords = await _studentRepository.CountAsync(parameters);
            var studentDtos =
                _mapper.Map<IEnumerable<StudentDto>>(students);

            var pagination = new PaginationMetadata
            {
                PageNumber = parameters.PageNumber,
                PageSize = parameters.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(
        totalRecords / (double)parameters.PageSize)
            };

            return new ApiResponse<IEnumerable<StudentDto>>(
                true,
                "Students fetched successfully",
                studentDtos,
                pagination
                );
        }

        public async Task<ApiResponse<StudentDto>> GetStudentByIdAsync(int id)
        {
            var student = await _studentRepository.GetByIdAsync(id);

            if (student == null)
            {
                return new ApiResponse<StudentDto>(
                    false,
                    "Student not found",
                    null,
                    null
                );
            }

            var studentDto = _mapper.Map<StudentDto>(student);

            return new ApiResponse<StudentDto>(
                true,
                "Student fetched successfully",
                studentDto,
                null
            );
        }

        public async Task<ApiResponse<StudentDto>> CreateStudentAsync(
            CreateStudentDto dto)
        {
            var student = _mapper.Map<Student>(dto);

            await _studentRepository.AddAsync(student);

            var studentDto = _mapper.Map<StudentDto>(student);

            return new ApiResponse<StudentDto>(
                true,
                "Student created successfully",
                studentDto,
                null
            );
        }

        public async Task<ApiResponse<string>> UpdateStudentAsync(
            int id,
            UpdateStudentDto dto)
        {
            var existingStudent =
                await _studentRepository.GetByIdAsync(id);

            if (existingStudent == null)
            {
                return new ApiResponse<string>(
                    false,
                    "Student not found",
                    null,
                    null
                );
            }

            _mapper.Map(dto, existingStudent);

            await _studentRepository.UpdateAsync(existingStudent);

            return new ApiResponse<string>(
                true,
                "Student updated successfully",
                null,
                null
            );
        }

        public async Task<ApiResponse<string>> DeleteStudentAsync(int id)
        {
            var existingStudent =
                await _studentRepository.GetByIdAsync(id);

            if (existingStudent == null)
            {
                return new ApiResponse<string>(
                    false,
                    "Student not found",
                    null,
                    null
                );
            }

            await _studentRepository.DeleteAsync(existingStudent);

            return new ApiResponse<string>(
                true,
                "Student deleted successfully",
                null,
                null
            );
        }
    }
}