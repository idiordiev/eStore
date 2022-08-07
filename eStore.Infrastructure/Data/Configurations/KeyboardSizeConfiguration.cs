using eStore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Data.Configurations
{
    public class KeyboardSizeConfiguration : IEntityTypeConfiguration<KeyboardSize>
    {
        public void Configure(EntityTypeBuilder<KeyboardSize> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name)
                .HasMaxLength(100);
        }
    }
}