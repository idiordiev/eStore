﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                .Include(m => m.Manufacturer)
                .Include(m => m.ConnectionType)
                .Include(m => m.Backlight)
                .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Mouse>> GetAllAsync()
        {
            return await _context.Mouses
                .Include(m => m.Manufacturer)
                .Include(m => m.ConnectionType)
                .Include(m => m.Backlight)
                .ToListAsync();
        }

        public IEnumerable<Mouse> Query(Expression<Func<Mouse, bool>> predicate)
        {
            return _context.Mouses
                .Include(m => m.Manufacturer)
                .Include(m => m.ConnectionType)
                .Include(m => m.Backlight)
                .Where(predicate.Compile())
                .ToList();
        }

        public async Task AddAsync(Mouse entity)
        {
            await _context.Mouses.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var mouse = await GetByIdAsync(id);
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