using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStore.Infrastructure.Persistence.Repositories
{
    public class MouseRepository : IRepository<Mouse>
    {
        private readonly ApplicationContext _context;

        public MouseRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Mouse> GetByIdAsync(int id)
        {
            return await _context.Mouses
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Mouse>> GetAllAsync()
        {
            return await _context.Mouses
                .ToListAsync();
        }

        public IEnumerable<Mouse> Query(Func<Mouse, bool> predicate)
        {
            return _context.Mouses
                .Where(predicate)
                .ToList();
        }

        public async Task AddAsync(Mouse entity)
        {
            await _context.Mouses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            Mouse mouse = await GetByIdAsync(id);
            _context.Mouses.Remove(mouse);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Mouse entity)
        {
            _context.Mouses.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}