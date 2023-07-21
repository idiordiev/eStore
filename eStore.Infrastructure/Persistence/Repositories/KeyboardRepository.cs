using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStore.Infrastructure.Persistence.Repositories;

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
            .Include(k => k.Switch)
            .FirstOrDefaultAsync(k => k.Id == id);
    }

    public async Task<IEnumerable<Keyboard>> GetAllAsync()
    {
        return await _context.Keyboards
            .Include(k => k.Switch)
            .ToListAsync();
    }

    public async Task<IEnumerable<Keyboard>> QueryAsync(Expression<Func<Keyboard, bool>> predicate)
    {
        return await _context.Keyboards
            .Include(k => k.Switch)
            .Where(predicate)
            .ToListAsync();
    }

    public async Task AddAsync(Keyboard entity)
    {
        await _context.Keyboards.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Keyboard keyboard = await GetByIdAsync(id);
        _context.Keyboards.Remove(keyboard);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Keyboard entity)
    {
        _context.Keyboards.Update(entity);
        await _context.SaveChangesAsync();
    }
}