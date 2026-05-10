using MediatR;
using SchoolManagement.Application.Common;
using SchoolManagement.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Students.Queries
{
    public class GetStudentsQuery
        : IRequest<ApiResponse<IEnumerable<StudentDto>>>
    {
        public StudentQueryParameters Parameters { get; set; }

        public GetStudentsQuery(
            StudentQueryParameters parameters)
        {
            Parameters = parameters;
        }
    }
}
