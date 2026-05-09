using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Data;


namespace SchoolManagement.Infrastructure.Repositories
{
    public class StudentRepository
    : GenericRepository<Student>,
      IStudentRepository
    {
        public StudentRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<List<Student>> GetAllAsync(StudentQueryParameters parameters)
        {
            var query = _context.Students.AsQueryable();

            // Search
            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                query = query.Where(x =>
                    x.FirstName.Contains(parameters.Search) ||
                    x.LastName.Contains(parameters.Search));
            }

            // Filter
            if (!string.IsNullOrWhiteSpace(parameters.Gender))
            {
                query = query.Where(x =>
                    x.Gender == parameters.Gender);
            }

            // Sorting
            if (!string.IsNullOrWhiteSpace(parameters.SortBy))
            {
                query = parameters.SortBy.ToLower() switch
                {
                    "firstname" => parameters.SortOrder == "desc"
                        ? query.OrderByDescending(x => x.FirstName)
                        : query.OrderBy(x => x.FirstName),

                    "lastname" => parameters.SortOrder == "desc"
                        ? query.OrderByDescending(x => x.LastName)
                        : query.OrderBy(x => x.LastName),

                    "email" => parameters.SortOrder == "desc"
                        ? query.OrderByDescending(x => x.Email)
                        : query.OrderBy(x => x.Email),

                    _ => query.OrderBy(x => x.Id)
                };
            }

            // Pagination
            query = query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

            return await query.ToListAsync();
        }

        public async Task<int> CountAsync(StudentQueryParameters parameters)
        {
            var query = _context.Students.AsQueryable();

            if (!string.IsNullOrWhiteSpace(parameters.Search))
            {
                query = query.Where(x =>
                    x.FirstName.Contains(parameters.Search) ||
                    x.LastName.Contains(parameters.Search));
            }

            if (!string.IsNullOrWhiteSpace(parameters.Gender))
            {
                query = query.Where(x =>
                    x.Gender == parameters.Gender);
            }

            return await query.CountAsync();
        }

    }
}
