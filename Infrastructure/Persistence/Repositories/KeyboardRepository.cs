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
    public class KeyboardRepository : IRepository<Keyboard>
    {
        private readonly ApplicationContext _context;

        public KeyboardRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Keyboard> GetByIdAsync(int id)
        {
            return await _context.Keyboards
                .Include(k => k.Manufacturer)
                .Include(k => k.ConnectionType)
                .Include(k => k.Switch)
                .ThenInclude(sw => sw.Manufacturer)
                .Include(k => k.Type)
                .Include(k => k.Size)
                .Include(k => k.KeycapMaterial)
                .Include(k => k.FrameMaterial)
                .Include(k => k.Backlight)
                .Include(k => k.KeyRollover)
                .FirstOrDefaultAsync(k => k.Id == id);
        }

        public async Task<IEnumerable<Keyboard>> GetAllAsync()
        {
            return await _context.Keyboards
                .Include(k => k.Manufacturer)
                .Include(k => k.ConnectionType)
                .Include(k => k.Switch)
                .ThenInclude(sw => sw.Manufacturer)
                .Include(k => k.Type)
                .Include(k => k.Size)
                .Include(k => k.KeycapMaterial)
                .Include(k => k.FrameMaterial)
                .Include(k => k.Backlight)
                .Include(k => k.KeyRollover)
                .ToListAsync();
        }

        public IEnumerable<Keyboard> Query(Expression<Func<Keyboard, bool>> predicate)
        {
            return _context.Keyboards
                .Include(k => k.Manufacturer)
                .Include(k => k.ConnectionType)
                .Include(k => k.Switch)
                .ThenInclude(sw => sw.Manufacturer)
                .Include(k => k.Type)
                .Include(k => k.Size)
                .Include(k => k.KeycapMaterial)
                .Include(k => k.FrameMaterial)
                .Include(k => k.Backlight)
                .Include(k => k.KeyRollover)
                .Where(predicate.Compile())
                .ToList();
        }

        public async Task AddAsync(Keyboard entity)
        {
            await _context.Keyboards.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var keyboard = await GetByIdAsync(id);
            _context.Keyboards.Remove(keyboard);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Keyboard entity)
        {
            _context.Keyboards.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}