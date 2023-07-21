using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Persistence.Configurations;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(o => o.Id);
        builder.Property(o => o.Status)
            .HasConversion<int>();
        builder.Property(o => o.Total)
            .HasColumnType("decimal(18,2)");
        builder.Property(o => o.ShippingCountry)
            .HasMaxLength(100);
        builder.Property(o => o.ShippingCity)
            .HasMaxLength(100);
        builder.Property(o => o.ShippingAddress)
            .HasMaxLength(200);
        builder.Property(o => o.ShippingPostalCode)
            .HasMaxLength(10);
        builder.HasOne(o => o.Customer)
            .WithMany(c => c.Orders)
            .HasForeignKey(o => o.CustomerId);
    }
}