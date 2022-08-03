using eStore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Data.Configurations
{
    public class KeyboardConfiguration : IEntityTypeConfiguration<Keyboard>
    {
        public void Configure(EntityTypeBuilder<Keyboard> builder)
        {
            builder.ToTable("Keyboards");
            builder.Property(k => k.Type)
                .HasConversion<int>();
            builder.Property(k => k.Size)
                .HasConversion<int>();
            builder.Property(k => k.ConnectionType)
                .HasConversion<int>();
            builder.Property(k => k.KeycapMaterial)
                .HasConversion<int>();
            builder.Property(k => k.FrameMaterial)
                .HasConversion<int>();
            builder.Property(k => k.KeyRollover)
                .HasConversion<int>();
            builder.Property(k => k.Backlight)
                .HasConversion<int>();
            builder.HasOne(k => k.Switch)
                .WithMany(sw => sw.Keyboards)
                .HasForeignKey(k => k.SwitchId);
        }
    }
}