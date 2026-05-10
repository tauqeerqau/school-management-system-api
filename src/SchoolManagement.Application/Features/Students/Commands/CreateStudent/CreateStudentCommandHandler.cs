using AutoMapper;
using MediatR;
using SchoolManagement.Application.Common;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommandHandler
        : IRequestHandler<
            CreateStudentCommand,
            ApiResponse<StudentDto>>
    {
        private readonly IStudentRepository
            _studentRepository;

        private readonly IMapper
            _mapper;

        public CreateStudentCommandHandler(
            IStudentRepository studentRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<StudentDto>> Handle(
            CreateStudentCommand request,
            CancellationToken cancellationToken)
        {
            var student =
                _mapper.Map<Student>(request.Student);

            var createdStudent =
                await _studentRepository.AddAsync(student);

            var studentDto =
                _mapper.Map<StudentDto>(createdStudent);

            return new ApiResponse<StudentDto>(
                true,
                "Student created successfully",
                studentDto,
                null);
        }
    }
}
