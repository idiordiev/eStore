using eStore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Data.Configurations
{
    public class MouseConfiguration : IEntityTypeConfiguration<Mouse>
    {
        public void Configure(EntityTypeBuilder<Mouse> builder)
        {
            builder.ToTable("Mouses");
            builder
                .Property(m => m.ConnectionType)
                .HasConversion<int>();
            builder.Property(m => m.SensorName)
                .HasMaxLength(100);
            builder.Property(m => m.Backlight)
                .HasConversion<int>();
        }
    }
}