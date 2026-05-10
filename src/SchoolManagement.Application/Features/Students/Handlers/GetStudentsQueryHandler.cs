using AutoMapper;
using MediatR;
using SchoolManagement.Application.Common;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Features.Students.Queries;
using SchoolManagement.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolManagement.Application.Features.Students.Handlers
{
    public class GetStudentsQueryHandler
        : IRequestHandler<
            GetStudentsQuery,
            ApiResponse<IEnumerable<StudentDto>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public GetStudentsQueryHandler(
            IStudentRepository studentRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<ApiResponse<IEnumerable<StudentDto>>> Handle(
            GetStudentsQuery request,
            CancellationToken cancellationToken)
        {
            var students =
                await _studentRepository
                    .GetAllAsync(request.Parameters);

            var totalRecords =
                await _studentRepository
                    .CountAsync(request.Parameters);

            var studentDtos =
                _mapper.Map<IEnumerable<StudentDto>>(students);

            var pagination = new PaginationMetadata
            {
                PageNumber = request.Parameters.PageNumber,
                PageSize = request.Parameters.PageSize,
                TotalRecords = totalRecords,
                TotalPages = (int)Math.Ceiling(
                    totalRecords /
                    (double)request.Parameters.PageSize)
            };
            await Task.Delay(2000);
            return new ApiResponse<IEnumerable<StudentDto>>(
                true,
                "Students fetched successfully",
                studentDtos,
                pagination
            );
        }
    }
}
