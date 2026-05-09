using Microsoft.EntityFrameworkCore;
using SchoolManagement.Application.DTOs;
using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Data;


namespace SchoolManagement.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private readonly ApplicationDbContext _context;

        public StudentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetAllAsync(
    StudentQueryParameters parameters)
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

        public async Task<Student?> GetByIdAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> AddAsync(Student student)
        {
            _context.Students.Add(student);

            await _context.SaveChangesAsync();

            return student;
        }

        public async Task UpdateAsync(Student student)
        {
            _context.Students.Update(student);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Student student)
        {
            _context.Students.Remove(student);

            await _context.SaveChangesAsync();
        }
    }
}
