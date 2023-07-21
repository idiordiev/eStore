using System.Collections.Generic;

namespace eStore.Domain.Entities;

public class ShoppingCart : Entity
{
    public int CustomerId { get; set; }

    public Customer Customer { get; set; }
    public ICollection<Goods> Goods { get; set; }
}