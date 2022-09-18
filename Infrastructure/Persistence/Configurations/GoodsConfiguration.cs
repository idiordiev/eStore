using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Persistence.Configurations
{
    public class GoodsConfiguration : IEntityTypeConfiguration<Goods>
    {
        public void Configure(EntityTypeBuilder<Goods> builder)
        {
            builder.HasKey(g => g.Id);
            builder.Property(g => g.Name)
                .HasMaxLength(150);
            builder.Property(g => g.Price)
                .HasColumnType("decimal(18,2)");
            builder.Property(g => g.Created)
                .HasColumnType("datetime");
            builder.Property(g => g.LastModified)
                .HasColumnType("datetime");
            builder.HasOne(g => g.Manufacturer)
                .WithMany()
                .HasForeignKey(g => g.ManufacturerId);
        }
    }
}