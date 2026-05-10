using MediatR;
using SchoolManagement.Application.Common;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Students.Commands.CreateStudent
{
    public class CreateStudentCommand
        : IRequest<ApiResponse<StudentDto>>
    {
        public CreateStudentDto Student { get; set; }

        public CreateStudentCommand(
            CreateStudentDto student)
        {
            Student = student;
        }
    }
}
