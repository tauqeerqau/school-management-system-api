using System;
using System.Collections.Generic;
using System.Text;

namespace SchoolManagement.Application.Interfaces
{
    public interface IGenericRepository<T>
        where T : class
    {
        Task<T?> GetByIdAsync(int id);

        Task<T> AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);
    }
}
