using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.Application.Interfaces.Data;
using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStore.Infrastructure.Persistence.Repositories
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
                .Include(oi => oi.Order)
                .FirstOrDefaultAsync(oi => oi.Id == id);
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems
                .Include(oi => oi.Goods)
                .Include(oi => oi.Order)
                .ToListAsync();
        }

        public IEnumerable<OrderItem> Query(Expression<Func<OrderItem, bool>> predicate)
        {
            return _context.OrderItems
                .Include(oi => oi.Goods)
                .Include(oi => oi.Order)
                .Where(predicate.Compile())
                .ToList();
        }

        public async Task AddAsync(OrderItem entity)
        {
            await _context.OrderItems.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            OrderItem orderItem = await GetByIdAsync(id);
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