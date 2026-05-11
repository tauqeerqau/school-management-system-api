using AutoMapper;
using MediatR;
using SchoolManagement.Application.Common;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;

namespace SchoolManagement.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandHandler
        : IRequestHandler<
            CreateStudentCommand,
            ApiResponse<StudentDto>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly ICacheService _cacheService;

        public CreateStudentCommandHandler(
            IStudentRepository studentRepository,
            IMapper mapper,
            ICacheService cacheService)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<ApiResponse<StudentDto>> Handle(
            CreateStudentCommand request,
            CancellationToken cancellationToken)
        {
            // Map DTO to Entity

            var student =
                _mapper.Map<Student>(request.Student);

            // Save to Database

            var createdStudent =
                await _studentRepository.AddAsync(student);

            // Invalidate Students Cache

            await _cacheService.RemoveByPatternAsync(
                "students_");

            // Map Entity to DTO

            var studentDto =
                _mapper.Map<StudentDto>(createdStudent);

            // Return Response

            return new ApiResponse<StudentDto>(
                true,
                "Student created successfully",
                studentDto,
                null);
        }
    }
}