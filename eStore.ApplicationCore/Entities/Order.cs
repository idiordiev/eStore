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
        public string ShippingCountry { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPostalCode { get; set; }

        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is Order other)
            {
                return this.Id == other.Id
                       && this.IsDeleted == other.IsDeleted
                       && this.TimeStamp == other.TimeStamp
                       && this.CustomerId == other.CustomerId
                       && this.Status == other.Status
                       && this.Total == other.Total
                       && this.ShippingCountry == other.ShippingCountry
                       && this.ShippingCity == other.ShippingCity
                       && this.ShippingAddress == other.ShippingAddress
                       && this.ShippingPostalCode == other.ShippingPostalCode;
            }

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