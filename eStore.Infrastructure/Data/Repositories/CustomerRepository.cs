using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Interfaces.Data;
using Microsoft.EntityFrameworkCore;

namespace eStore.Infrastructure.Data.Repositories
{
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
                .ThenInclude(g => g.Goods)
                .ThenInclude(g => g.Manufacturer)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => g.Goods.ConnectionType)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).Backlight)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).Type)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).Size)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).KeyRollover)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).KeycapMaterial)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).FrameMaterial)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).Switch.Manufacturer)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Mouse).Backlight)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Mousepad).Backlight)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Mousepad).TopMaterial)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Mousepad).BottomMaterial)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Gamepad).Feedback)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Gamepad).CompatibleDevices)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _context.Customers
                .Include(c => c.ShoppingCart)
                .ThenInclude(c => c.Goods)
                .ThenInclude(g => g.Goods)
                .ThenInclude(g => g.Manufacturer)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => g.Goods.ConnectionType)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).Backlight)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).Type)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).Size)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).KeyRollover)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).KeycapMaterial)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).FrameMaterial)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).Switch.Manufacturer)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Mouse).Backlight)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Mousepad).Backlight)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Mousepad).TopMaterial)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Mousepad).BottomMaterial)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Gamepad).Feedback)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Gamepad).CompatibleDevices)
                .ToListAsync();
        }

        public IEnumerable<Customer> Query(Func<Customer, bool> predicate)
        {
            return _context.Customers
                .Include(c => c.ShoppingCart)
                .ThenInclude(c => c.Goods)
                .ThenInclude(g => g.Goods)
                .ThenInclude(g => g.Manufacturer)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => g.Goods.ConnectionType)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).Backlight)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).Type)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).Size)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).KeyRollover)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).KeycapMaterial)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).FrameMaterial)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Keyboard).Switch.Manufacturer)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Mouse).Backlight)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Mousepad).Backlight)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Mousepad).TopMaterial)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Mousepad).BottomMaterial)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Gamepad).Feedback)
                .Include(c => c.ShoppingCart.Goods)
                .ThenInclude(g => (g.Goods as Gamepad).CompatibleDevices)
                .Where(predicate);
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
}