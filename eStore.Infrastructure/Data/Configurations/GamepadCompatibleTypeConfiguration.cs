using eStore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Data.Configurations
{
    public class GamepadCompatibleTypeConfiguration : IEntityTypeConfiguration<GamepadCompatibleType>
    {
        public void Configure(EntityTypeBuilder<GamepadCompatibleType> builder)
        {
            builder.HasKey(g => g.Id);
            builder.HasOne(g => g.Gamepad)
                .WithMany(gamepad => gamepad.CompatibleWithPlatforms)
                .HasForeignKey(g => g.GamepadId);
        }
    }
}