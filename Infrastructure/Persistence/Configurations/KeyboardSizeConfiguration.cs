using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Persistence.Configurations
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