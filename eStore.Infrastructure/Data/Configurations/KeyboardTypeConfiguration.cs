using eStore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Data.Configurations
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