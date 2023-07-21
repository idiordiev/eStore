using System;
using System.Collections.Generic;

namespace eStore.Domain.Entities;

public abstract class Goods : Entity
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string BigImageUrl { get; set; }
    public DateTime Created { get; set; }
    public DateTime LastModified { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
    public ICollection<ShoppingCart> ShoppingCarts { get; set; }
}