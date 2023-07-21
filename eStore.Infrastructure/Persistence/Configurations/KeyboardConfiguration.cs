using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Persistence.Configurations;

public class KeyboardConfiguration : IEntityTypeConfiguration<Keyboard>
{
    public void Configure(EntityTypeBuilder<Keyboard> builder)
    {
        builder.ToTable("Keyboards");
        builder.Property(k => k.Type)
            .HasMaxLength(50);
        builder.Property(k => k.Size)
            .HasMaxLength(50);
        builder.HasOne(k => k.Switch)
            .WithMany(sw => sw.Keyboards)
            .HasForeignKey(k => k.SwitchId);
        builder.Property(k => k.KeycapMaterial)
            .HasMaxLength(100);
        builder.Property(k => k.FrameMaterial)
            .HasMaxLength(50);
        builder.Property(k => k.KeyRollover)
            .HasMaxLength(20);
        builder.Property(k => k.Backlight)
            .HasMaxLength(50);
        builder.Property(k => k.ConnectionType)
            .HasMaxLength(50);
    }
}