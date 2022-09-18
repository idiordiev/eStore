using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Persistence.Configurations
{
    public class KeyboardTypeConfiguration : IEntityTypeConfiguration<KeyboardType>
    {
        public void Configure(EntityTypeBuilder<KeyboardType> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(t => t.Name)
                .HasMaxLength(100);
        }
    }
}