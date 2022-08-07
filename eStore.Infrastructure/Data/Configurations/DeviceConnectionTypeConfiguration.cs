using eStore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Data.Configurations
{
    public class DeviceConnectionTypeConfiguration : IEntityTypeConfiguration<DeviceConnectionType>
    {
        public void Configure(EntityTypeBuilder<DeviceConnectionType> builder)
        {
            builder.HasKey(d => new { d.GoodsId, d.ConnectionTypeId });
            builder.HasOne(d => d.Goods)
                .WithMany(g => g.ConnectionTypes)
                .HasForeignKey(d => d.GoodsId);
            builder.HasOne(d => d.ConnectionType)
                .WithMany(c => c.Goods)
                .HasForeignKey(d => d.ConnectionTypeId);
        }
    }
}