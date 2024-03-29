﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStore.Infrastructure.Persistence.Repositories;

public class CustomerRepository : IRepository<Customer>
{
    private readonly ApplicationContext _context;

    public CustomerRepository(ApplicationContext context)
    {
        _context = context;
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        return await _context.Customers
            .Include(c => c.ShoppingCart)
            .ThenInclude(c => c.Goods)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _context.Customers
            .Include(c => c.ShoppingCart)
            .ThenInclude(c => c.Goods)
            .ToListAsync();
    }

    public async Task<IEnumerable<Customer>> QueryAsync(Expression<Func<Customer, bool>> predicate)
    {
        return await _context.Customers
            .Include(c => c.ShoppingCart)
            .ThenInclude(c => c.Goods)
            .Where(predicate)
            .ToListAsync();
    }

    public async Task AddAsync(Customer entity)
    {
        await _context.Customers.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var customer = await GetByIdAsync(id);
        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Customer entity)
    {
        _context.Customers.Update(entity);
        await _context.SaveChangesAsync();
    }
}