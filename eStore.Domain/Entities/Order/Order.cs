using System;
using System.Collections.Generic;
using eStore.Domain.Entities.Common;

namespace eStore.Domain.Entities.Order
{
    public class Order
    {
        public DateTime TimeStamp { get; private set; }
        public Customer.Customer Customer { get; private set; }
        public OrderStatus Status { get; private set; }
        public decimal Total { get; private set; }
        public Address ShippingAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; private set; }

        public Order()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}