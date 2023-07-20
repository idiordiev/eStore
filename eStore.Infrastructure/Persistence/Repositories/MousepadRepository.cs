using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStore.Infrastructure.Persistence.Repositories
{
    public class MousepadRepository : IRepository<Mousepad>
    {
        private readonly ApplicationContext _context;

        public MousepadRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Mousepad> GetByIdAsync(int id)
        {
            return await _context.Mousepads
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Mousepad>> GetAllAsync()
        {
            return await _context.Mousepads
                .ToListAsync();
        }

        public IEnumerable<Mousepad> Query(Expression<Func<Mousepad, bool>> predicate)
        {
            return _context.Mousepads
                .Where(predicate.Compile())
                .ToList();
        }

        public async Task AddAsync(Mousepad entity)
        {
            await _context.Mousepads.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Mousepad mousepad = await GetByIdAsync(id);
            _context.Mousepads.Remove(mousepad);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Mousepad entity)
        {
            _context.Mousepads.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}