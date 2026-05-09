using SchoolManagement.Application.Interfaces;
using SchoolManagement.Domain.Entities;
using SchoolManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Infrastructure.Repositories
{
    public class UserRepository
        : GenericRepository<AppUser>,
          IUserRepository
    {
        public UserRepository(
            ApplicationDbContext context)
            : base(context)
        {
        }

        public async Task<AppUser?> GetByEmailAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<AppUser?> GetByRefreshTokenAsync(string refreshToken)
        {
            return await _context.Users
                .FirstOrDefaultAsync(
                    x => x.RefreshToken == refreshToken);
        }
    }
}
