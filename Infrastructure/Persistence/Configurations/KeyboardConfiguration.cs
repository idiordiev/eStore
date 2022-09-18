using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Persistence.Configurations
{
    public class KeyboardConfiguration : IEntityTypeConfiguration<Keyboard>
    {
        public void Configure(EntityTypeBuilder<Keyboard> builder)
        {
            builder.ToTable("Keyboards");
            builder.HasOne(k => k.Type)
                .WithMany(t => t.Keyboards)
                .HasForeignKey(k => k.TypeId);
            builder.HasOne(k => k.Size)
                .WithMany(s => s.Keyboards)
                .HasForeignKey(k => k.SizeId);
            builder.HasOne(k => k.Switch)
                .WithMany(sw => sw.Keyboards)
                .HasForeignKey(k => k.SwitchId);
            builder.HasOne(k => k.KeycapMaterial)
                .WithMany()
                .HasForeignKey(k => k.KeycapMaterialId);
            builder.HasOne(k => k.FrameMaterial)
                .WithMany()
                .HasForeignKey(k => k.FrameMaterialId);
            builder.HasOne(k => k.KeyRollover)
                .WithMany(k => k.Keyboards)
                .HasForeignKey(k => k.KeyRolloverId);
            builder.HasOne(k => k.Backlight)
                .WithMany()
                .HasForeignKey(k => k.BacklightId);
            builder.HasOne(k => k.ConnectionType)
                .WithMany()
                .HasForeignKey(k => k.ConnectionTypeId);
        }
    }
}