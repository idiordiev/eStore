using eStore.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace eStore.Infrastructure.Data.Configurations
{
    public class GoodsInCartConfiguration : IEntityTypeConfiguration<GoodsInCart>
    {
        public void Configure(EntityTypeBuilder<GoodsInCart> builder)
        {
            builder.HasKey(g => new { g.CartId, g.GoodsId });
            builder.HasOne(g => g.Goods)
                .WithMany(g => g.GoodsInCarts)
                .HasForeignKey(g => g.GoodsId);
            builder.HasOne(g => g.Cart)
                .WithMany(c => c.Goods)
                .HasForeignKey(g => g.CartId);
        }
    }
}