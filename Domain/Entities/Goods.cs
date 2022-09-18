using System;
using System.Collections.Generic;

namespace eStore.Domain.Entities
{
    public abstract class Goods : Entity
    {
        public Goods()
        {
            OrderItems = new List<OrderItem>();
            GoodsInCarts = new List<GoodsInCart>();
        }

        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ThumbnailImageUrl { get; set; }
        public string BigImageUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }
        public virtual ICollection<OrderItem> OrderItems { get; set; }
        public virtual ICollection<GoodsInCart> GoodsInCarts { get; set; }
    }
}