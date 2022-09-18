using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Persistence.Configurations
{
    public class KeyboardSwitchConfiguration : IEntityTypeConfiguration<KeyboardSwitch>
    {
        public void Configure(EntityTypeBuilder<KeyboardSwitch> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.Name)
                .HasMaxLength(100);
            builder.HasOne(s => s.Manufacturer)
                .WithMany()
                .HasForeignKey(s => s.ManufacturerId);
        }
    }
}