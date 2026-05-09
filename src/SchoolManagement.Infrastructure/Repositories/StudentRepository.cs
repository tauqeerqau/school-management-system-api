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

            // Pagination
            query = query
                .Skip((parameters.PageNumber - 1) * parameters.PageSize)
                .Take(parameters.PageSize);

            return await query.ToListAsync();
        }

    }
}
