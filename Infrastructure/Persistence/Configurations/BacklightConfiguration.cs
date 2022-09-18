using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Persistence.Configurations
{
    public class BacklightConfiguration : IEntityTypeConfiguration<Backlight>
    {
        public void Configure(EntityTypeBuilder<Backlight> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Name)
                .HasMaxLength(100);
        }
    }
}