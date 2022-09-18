using eStore.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Persistence.Configurations
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