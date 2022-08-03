using eStore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Data.Configurations
{
    public class MousepadConfiguration : IEntityTypeConfiguration<Mousepad>
    {
        public void Configure(EntityTypeBuilder<Mousepad> builder)
        {
            builder.ToTable("Mousepads");
            builder.Property(m => m.BottomMaterial)
                .HasConversion<int>();
            builder.Property(m => m.TopMaterial)
                .HasConversion<int>();
        }
    }
}