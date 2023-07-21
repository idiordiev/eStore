using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStore.Infrastructure.Persistence.Repositories;

public class GoodsRepository : IRepository<Goods>
{
    private readonly ApplicationContext _context;

    public GoodsRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Goods> GetByIdAsync(int id)
    {
        return await _context.Goods
            .FirstOrDefaultAsync(g => g.Id == id);
    }

    public async Task<IEnumerable<Goods>> GetAllAsync()
    {
        return await _context.Goods
            .ToListAsync();
    }

    public async Task<IEnumerable<Goods>> QueryAsync(Expression<Func<Goods, bool>> predicate)
    {
        return await _context.Goods
            .Where(predicate)
            .ToListAsync();
    }

    public async Task AddAsync(Goods entity)
    {
        await _context.Goods.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        Goods goods = await GetByIdAsync(id);
        _context.Goods.Remove(goods);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Goods entity)
    {
        _context.Goods.Update(entity);
        await _context.SaveChangesAsync();
    }
}