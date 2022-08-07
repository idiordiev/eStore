using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace eStore.Infrastructure.Data.Repositories
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
                .Include(m => m.Manufacturer)
                .Include(m => m.ConnectionTypes)
                .ThenInclude(t => t.ConnectionType)
                .Include(m => m.BottomMaterial)
                .Include(m => m.TopMaterial)
                .Include(m => m.Backlight)
                .FirstAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Mousepad>> GetAllAsync()
        {
            return await _context.Mousepads
                .Include(m => m.Manufacturer)
                .Include(m => m.ConnectionTypes)
                .ThenInclude(t => t.ConnectionType)
                .Include(m => m.BottomMaterial)
                .Include(m => m.TopMaterial)
                .Include(m => m.Backlight)
                .ToListAsync();
        }

        public IEnumerable<Mousepad> Query(Func<Mousepad, bool> predicate)
        {
            return _context.Mousepads
                .Include(m => m.Manufacturer)
                .Include(m => m.ConnectionTypes)
                .ThenInclude(t => t.ConnectionType)
                .Include(m => m.BottomMaterial)
                .Include(m => m.TopMaterial)
                .Include(m => m.Backlight)
                .Where(predicate)
                .ToList();
        }

        public async Task AddAsync(Mousepad entity)
        {
            await _context.Mousepads.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var mousepad = await GetByIdAsync(id);
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