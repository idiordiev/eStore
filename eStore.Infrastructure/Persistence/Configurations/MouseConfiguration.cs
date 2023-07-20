using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Persistence.Configurations
{
    public class MouseConfiguration : IEntityTypeConfiguration<Mouse>
    {
        public void Configure(EntityTypeBuilder<Mouse> builder)
        {
            builder.ToTable("Mouses");
            builder.Property(m => m.Backlight)
                .HasMaxLength(50);
            builder.Property(m => m.ConnectionType)
                .HasMaxLength(50);
        }
    }
}