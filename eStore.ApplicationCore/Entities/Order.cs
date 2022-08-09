using System;
using System.Collections.Generic;
using eStore.ApplicationCore.Enums;

namespace eStore.ApplicationCore.Entities
{
    public class Order : Entity
    {
        public Order()
        {
            OrderItems = new List<OrderItem>();
        }

        public DateTime TimeStamp { get; set; }
        public int CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPostalCode { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}