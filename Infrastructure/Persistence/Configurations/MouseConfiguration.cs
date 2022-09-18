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
            builder.HasOne(m => m.Backlight)
                .WithMany()
                .HasForeignKey(m => m.BacklightId);
            builder.HasOne(m => m.ConnectionType)
                .WithMany()
                .HasForeignKey(m => m.ConnectionTypeId);
        }
    }
}