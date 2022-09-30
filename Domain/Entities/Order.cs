using System;
using System.Collections.Generic;
using eStore.Domain.Enums;

namespace eStore.Domain.Entities
{
    public class Order : Entity
    {
        public DateTime TimeStamp { get; set; }
        public int CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public decimal Total { get; set; }
        public string ShippingCountry { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPostalCode { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Order other)
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && TimeStamp == other.TimeStamp
                       && CustomerId == other.CustomerId
                       && Status == other.Status
                       && Total == other.Total
                       && ShippingCountry == other.ShippingCountry
                       && ShippingCity == other.ShippingCity
                       && ShippingAddress == other.ShippingAddress
                       && ShippingPostalCode == other.ShippingPostalCode;

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * IsDeleted.GetHashCode() * TimeStamp.GetHashCode() * CustomerId.GetHashCode()
                       * Status.GetHashCode() * Total.GetHashCode() * ShippingCity.GetHashCode()
                       * ShippingAddress.GetHashCode() * ShippingPostalCode.GetHashCode();
            }
        }
    }
}