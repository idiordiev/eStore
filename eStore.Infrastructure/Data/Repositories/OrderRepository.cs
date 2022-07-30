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
    public class OrderRepository : IRepository<Order>
    {
        private readonly ApplicationContext _context;

        public OrderRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Order> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Goods)
                .FirstAsync(o => o.Id == id);
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Goods)
                .ToListAsync();
        }

        public IEnumerable<Order> Query(Func<Order, bool> predicate)
        {
            return _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Goods)
                .Where(predicate)
                .ToList();
        }

        public async Task AddAsync(Order entity)
        {
            await _context.Orders.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var order = await GetByIdAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Order entity)
        {
            _context.Orders.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}