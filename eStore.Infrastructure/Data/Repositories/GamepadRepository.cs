﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace eStore.Infrastructure.Data.Repositories
{
    public class GamepadRepository : IRepository<Gamepad>
    {
        private readonly ApplicationContext _context;

        public GamepadRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Gamepad> GetByIdAsync(int id)
        {
            return await _context.Gamepads
                .Include(g => g.Manufacturer)
                .Include(g => g.CompatibleWithPlatforms)
                .FirstAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Gamepad>> GetAllAsync()
        {
            return await _context.Gamepads
                .Include(g => g.Manufacturer)
                .Include(g => g.CompatibleWithPlatforms)
                .ToListAsync();
        }

        public IEnumerable<Gamepad> Query(Func<Gamepad, bool> predicate)
        {
            return _context.Gamepads
                .Include(g => g.Manufacturer)
                .Include(g => g.CompatibleWithPlatforms)
                .Where(predicate)
                .ToList();
        }

        public async Task AddAsync(Gamepad entity)
        {
            await _context.Gamepads.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var gamepad = await GetByIdAsync(id);
            _context.Gamepads.Remove(gamepad);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Gamepad entity)
        {
            _context.Gamepads.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}