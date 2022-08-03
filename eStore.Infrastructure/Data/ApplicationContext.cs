using System.Reflection;
using eStore.ApplicationCore.Entities;
using eStore.Infrastructure.Data.Configurations;
using Microsoft.EntityFrameworkCore;

namespace eStore.Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Gamepad> Gamepads { get; set; }
        public DbSet<GamepadCompatibleType> GamepadCompatibleWiths { get; set; }
        public DbSet<Goods> Goods { get; set; }
        public DbSet<Keyboard> Keyboards { get; set; }
        public DbSet<KeyboardSwitch> Switches { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Mouse> Mouses { get; set; }
        public DbSet<Mousepad> Mousepads { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public ApplicationContext()
        {
        }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer(
                    @"Server=localhost\sqlexpress;Database=eStore;Trusted_Connection=True;MultipleActiveResultSets=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}