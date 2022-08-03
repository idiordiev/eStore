using eStore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Data.Configurations
{
    public class GamepadConfiguration : IEntityTypeConfiguration<Gamepad>
    {
        public void Configure(EntityTypeBuilder<Gamepad> builder)
        {
            builder.ToTable("Gamepads");
            builder.Property(g => g.ConnectionType)
                .HasConversion<int>();
            builder.Property(g => g.Feedback)
                .HasConversion<int>();
        }
    }
}