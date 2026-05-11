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
        private readonly ICacheService _cacheService;

        public GetStudentsQueryHandler(
            IStudentRepository studentRepository,
            IMapper mapper,
            ICacheService cacheService)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _cacheService = cacheService;
        }

        public async Task<ApiResponse<IEnumerable<StudentDto>>> Handle(
    GetStudentsQuery request,
    CancellationToken cancellationToken)
        {
            // Create unique cache key

            var cacheKey =
                $"students_" +
                $"{request.Parameters.PageNumber}_" +
                $"{request.Parameters.PageSize}_" +
                $"{request.Parameters.Search}_" +
                $"{request.Parameters.Gender}_" +
                $"{request.Parameters.SortBy}_" +
                $"{request.Parameters.SortOrder}";

            // Try getting data from Redis

            var cachedResponse =
                await _cacheService.GetAsync<
                    ApiResponse<IEnumerable<StudentDto>>>(cacheKey);

            if (cachedResponse != null)
            {
                return cachedResponse;
            }

            // Fetch from database

            var students =
                await _studentRepository.GetAllAsync(
                    request.Parameters);

            var studentDtos =
                _mapper.Map<IEnumerable<StudentDto>>(students);

            var response =
                new ApiResponse<IEnumerable<StudentDto>>(
                    true,
                    "Students fetched successfully",
                    studentDtos,
                    null);

            // Save to Redis

            await _cacheService.SetAsync(
                cacheKey,
                response,
                TimeSpan.FromMinutes(5));

            return response;
        }
    }
}
