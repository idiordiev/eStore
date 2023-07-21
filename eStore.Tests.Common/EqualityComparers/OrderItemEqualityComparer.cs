using System;
using System.Collections.Generic;
using eStore.Domain.Entities;

namespace eStore.Tests.Common.EqualityComparers;

public class OrderItemEqualityComparer : IEqualityComparer<OrderItem>
{
    public bool Equals(OrderItem x, OrderItem y)
    {
        if (ReferenceEquals(x, y))
        {
            return true;
        }

        if (ReferenceEquals(x, null))
        {
            return false;
        }

        if (ReferenceEquals(y, null))
        {
            return false;
        }

        if (x.GetType() != y.GetType())
        {
            return false;
        }

        return x.Id == y.Id
               && x.IsDeleted == y.IsDeleted
               && x.OrderId == y.OrderId
               && x.UnitPrice == y.UnitPrice
               && x.GoodsId == y.GoodsId
               && x.Quantity == y.Quantity;
    }

    public int GetHashCode(OrderItem obj)
    {
        return HashCode.Combine(obj.Id, obj.IsDeleted, obj.OrderId, obj.UnitPrice, obj.GoodsId, obj.Quantity);
    }
}