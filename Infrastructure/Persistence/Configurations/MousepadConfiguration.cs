using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Persistence.Configurations
{
    public class MousepadConfiguration : IEntityTypeConfiguration<Mousepad>
    {
        public void Configure(EntityTypeBuilder<Mousepad> builder)
        {
            builder.ToTable("Mousepads");
            builder.HasOne(m => m.BottomMaterial)
                .WithMany()
                .HasForeignKey(m => m.BottomMaterialId);
            builder.HasOne(m => m.TopMaterial)
                .WithMany()
                .HasForeignKey(m => m.TopMaterialId);
            builder.HasOne(m => m.Backlight)
                .WithMany()
                .HasForeignKey(m => m.BacklightId);
        }
    }
}