using System.Reflection;
using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace eStore.Infrastructure.Persistence
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gamepad> Gamepads { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Keyboard> Keyboards { get; set; }
        public DbSet<KeyboardSwitch> KeyboardSwitches { get; set; }
        public DbSet<Mouse> Mouses { get; set; }
        public DbSet<Mousepad> Mousepads { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}