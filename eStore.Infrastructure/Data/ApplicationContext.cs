using eStore.ApplicationCore.Entities;
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
            modelBuilder.Entity<Customer>()
                .HasKey(c => c.Id);
            modelBuilder.Entity<Customer>()
                .Property(c => c.IdentityId)
                .HasMaxLength(30);
            modelBuilder.Entity<Customer>()
                .Property(c => c.FirstName)
                .HasMaxLength(120);
            modelBuilder.Entity<Customer>()
                .Property(c => c.LastName)
                .HasMaxLength(120);
            modelBuilder.Entity<Customer>()
                .Property(c => c.DateOfBirth)
                .HasColumnType("date");
            modelBuilder.Entity<Customer>()
                .Property(c => c.Email)
                .HasMaxLength(100);
            modelBuilder.Entity<Customer>()
                .Property(c => c.PhoneNumber)
                .HasMaxLength(20);
            modelBuilder.Entity<Customer>()
                .Property(c => c.Country)
                .HasMaxLength(100);
            modelBuilder.Entity<Customer>()
                .Property(c => c.State)
                .HasMaxLength(2);
            modelBuilder.Entity<Customer>()
                .Property(c => c.City)
                .HasMaxLength(100);
            modelBuilder.Entity<Customer>()
                .Property(c => c.AddressLine1)
                .HasMaxLength(100);
            modelBuilder.Entity<Customer>()
                .Property(c => c.AddressLine2)
                .HasMaxLength(100);
            modelBuilder.Entity<Customer>()
                .Property(c => c.PostalCode)
                .HasMaxLength(10);
            
            modelBuilder.Entity<Goods>()
                .Property(g => g.Name)
                .HasMaxLength(150);
            modelBuilder.Entity<Goods>()
                .Property(g => g.Description)
                .HasMaxLength(500);
            modelBuilder.Entity<Goods>()
                .Property(g => g.Price)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Goods>()
                .Property(g => g.Created)
                .HasColumnType("datetime");
            modelBuilder.Entity<Goods>()
                .Property(g => g.LastModified)
                .HasColumnType("datetime");
            modelBuilder.Entity<Goods>()
                .HasOne(g => g.Manufacturer)
                .WithMany()
                .HasForeignKey(g => g.ManufacturerId);

            modelBuilder.Entity<Gamepad>().ToTable("Gamepads");
            modelBuilder.Entity<Gamepad>()
                .Property(g => g.ConnectionType)
                .HasConversion<int>();
            modelBuilder.Entity<Gamepad>()
                .Property(g => g.Feedback)
                .HasConversion<int>();

            modelBuilder.Entity<GamepadCompatibleType>()
                .HasKey(g => g.Id);
            modelBuilder.Entity<GamepadCompatibleType>()
                .HasOne(g => g.Gamepad)
                .WithMany(gamepad => gamepad.CompatibleWithPlatforms)
                .HasForeignKey(g => g.GamepadId);

            modelBuilder.Entity<Keyboard>().ToTable("Keyboards");
            modelBuilder.Entity<Keyboard>()
                .Property(k => k.Type)
                .HasConversion<int>();
            modelBuilder.Entity<Keyboard>()
                .Property(k => k.Size)
                .HasConversion<int>();
            modelBuilder.Entity<Keyboard>()
                .Property(k => k.ConnectionType)
                .HasConversion<int>();
            modelBuilder.Entity<Keyboard>()
                .Property(k => k.KeycapMaterial)
                .HasConversion<int>();
            modelBuilder.Entity<Keyboard>()
                .Property(k => k.FrameMaterial)
                .HasConversion<int>();
            modelBuilder.Entity<Keyboard>()
                .Property(k => k.KeyRollover)
                .HasConversion<int>();
            modelBuilder.Entity<Keyboard>()
                .Property(k => k.Backlight)
                .HasConversion<int>();
            modelBuilder.Entity<Keyboard>()
                .HasOne(k => k.Switch)
                .WithMany(sw => sw.Keyboards)
                .HasForeignKey(k => k.SwitchId);

            modelBuilder.Entity<KeyboardSwitch>()
                .HasKey(s => s.Id);
            modelBuilder.Entity<KeyboardSwitch>()
                .Property(s => s.Name)
                .HasMaxLength(100);
            modelBuilder.Entity<KeyboardSwitch>()
                .HasOne(s => s.Manufacturer)
                .WithMany()
                .HasForeignKey(s => s.ManufacturerId);

            modelBuilder.Entity<Manufacturer>()
                .HasKey(m => m.Id);

            modelBuilder.Entity<Mouse>().ToTable("Mouses");
            modelBuilder.Entity<Mouse>()
                .Property(m => m.ConnectionType)
                .HasConversion<int>();
            modelBuilder.Entity<Mouse>()
                .Property(m => m.SensorName)
                .HasMaxLength(100);
            modelBuilder.Entity<Mouse>()
                .Property(m => m.Backlight)
                .HasConversion<int>();

            modelBuilder.Entity<Mousepad>().ToTable("Mousepads");
            modelBuilder.Entity<Mousepad>()
                .Property(m => m.BottomMaterial)
                .HasConversion<int>();
            modelBuilder.Entity<Mousepad>()
                .Property(m => m.TopMaterial)
                .HasConversion<int>();

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);
            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .HasConversion<int>();
            modelBuilder.Entity<Order>()
                .Property(o => o.Total)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Order>()
                .Property(o => o.ShippingCountry)
                .HasMaxLength(100);
            modelBuilder.Entity<Order>()
                .Property(o => o.ShippingState)
                .HasMaxLength(2);
            modelBuilder.Entity<Order>()
                .Property(o => o.ShippingCity)
                .HasMaxLength(100);
            modelBuilder.Entity<Order>()
                .Property(o => o.ShippingAddressLine1)
                .HasMaxLength(100);
            modelBuilder.Entity<Order>()
                .Property(o => o.ShippingAddressLine2)
                .HasMaxLength(100);
            modelBuilder.Entity<Order>()
                .Property(o => o.ShippingPostalCode)
                .HasMaxLength(10);
            modelBuilder.Entity<Order>()
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);

            modelBuilder.Entity<OrderItem>()
                .HasKey(oi => oi.Id);
            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasColumnType("decimal(18,2)");
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Goods)
                .WithMany(g => g.OrderItems)
                .HasForeignKey(oi => oi.GoodsId);
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId);
        }
    }
}