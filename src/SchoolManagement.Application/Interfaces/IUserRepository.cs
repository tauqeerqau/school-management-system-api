using SchoolManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Interfaces
{
    public interface IUserRepository
        : IGenericRepository<AppUser>
    {
        Task<AppUser?> GetByEmailAsync(string email);
    }
}
