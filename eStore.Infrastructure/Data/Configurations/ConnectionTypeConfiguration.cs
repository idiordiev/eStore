using eStore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Data.Configurations
{
    public class ConnectionTypeConfiguration : IEntityTypeConfiguration<ConnectionType>
    {
        public void Configure(EntityTypeBuilder<ConnectionType> builder)
        {
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Name)
                .HasMaxLength(100);
        }
    }
}