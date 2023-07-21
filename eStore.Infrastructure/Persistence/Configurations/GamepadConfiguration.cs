using System.Collections.Generic;
using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace eStore.Infrastructure.Persistence.Configurations;

public class GamepadConfiguration : IEntityTypeConfiguration<Gamepad>
{
    public void Configure(EntityTypeBuilder<Gamepad> builder)
    {
        builder.ToTable("Gamepads");
        builder.Property(g => g.Feedback)
            .HasMaxLength(50);
        builder.Property(g => g.ConnectionType)
            .HasMaxLength(50);
        builder.Property(g => g.CompatibleDevices)
            .HasConversion(new ValueConverter<ICollection<string>, string>(v => JsonConvert.SerializeObject(v),
                v => JsonConvert.DeserializeObject<List<string>>(v)));
    }
}