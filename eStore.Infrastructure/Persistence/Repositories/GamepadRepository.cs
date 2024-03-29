﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStore.Infrastructure.Persistence.Repositories;

public class GamepadRepository : IRepository<Gamepad>
{
    private readonly ApplicationContext _context;

    public GamepadRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Gamepad> GetByIdAsync(int id)
    {
        return await _context.Gamepads.FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<Gamepad>> GetAllAsync()
    {
        return await _context.Gamepads
            .ToListAsync();
    }

    public async Task<IEnumerable<Gamepad>> QueryAsync(Expression<Func<Gamepad, bool>> predicate)
    {
        return await _context.Gamepads
            .Where(predicate)
            .ToListAsync();
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