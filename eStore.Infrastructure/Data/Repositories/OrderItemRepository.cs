using System;
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
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private readonly ApplicationContext _context;

        public OrderItemRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<OrderItem> GetByIdAsync(int id)
        {
            return await _context.OrderItems
                .Include(oi => oi.Goods)
                .ThenInclude(g => g.Manufacturer)
                .Include(oi => oi.Order)
                .FirstAsync(oi => oi.Id == id);
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems
                .Include(oi => oi.Goods)
                .ThenInclude(g => g.Manufacturer)
                .Include(oi => oi.Order)
                .ToListAsync();
        }

        public IEnumerable<OrderItem> Query(Func<OrderItem, bool> predicate)
        {
            return _context.OrderItems
                .Include(oi => oi.Goods)
                .ThenInclude(g => g.Manufacturer)
                .Include(oi => oi.Order)
                .Where(predicate)
                .ToList();
        }

        public async Task AddAsync(OrderItem entity)
        {
            await _context.OrderItems.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var orderItem = await GetByIdAsync(id);
            _context.OrderItems.Remove(orderItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(OrderItem entity)
        {
            _context.OrderItems.Update(entity);
            await _context.SaveChangesAsync();
        }
    }
}