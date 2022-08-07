using eStore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Data.Configurations
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