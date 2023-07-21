using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using eStore.Domain.Entities;

namespace eStore.Application.Interfaces.Data
{
    public interface IRepository<T> where T : Entity
    {
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> Query(Func<T, bool> predicate);
        Task AddAsync(T entity);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);
    }
}