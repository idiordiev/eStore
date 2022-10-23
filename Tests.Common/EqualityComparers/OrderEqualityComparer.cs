using System;
using System.Collections.Generic;
using eStore.Domain.Entities;

namespace eStore.Tests.Common.EqualityComparers
{
    public class OrderEqualityComparer : IEqualityComparer<Order>
    {
        public bool Equals(Order x, Order y)
        {
            if (ReferenceEquals(x, y)) 
                return true;
            
            if (ReferenceEquals(x, null)) 
                return false;
            
            if (ReferenceEquals(y, null)) 
                return false;
            
            if (x.GetType() != y.GetType()) 
                return false;
            
            return x.Id == y.Id 
                   && x.IsDeleted == y.IsDeleted 
                   && x.TimeStamp.Equals(y.TimeStamp) 
                   && x.CustomerId == y.CustomerId 
                   && x.Status == y.Status 
                   && x.Total == y.Total 
                   && x.ShippingCountry == y.ShippingCountry 
                   && x.ShippingCity == y.ShippingCity 
                   && x.ShippingAddress == y.ShippingAddress 
                   && x.ShippingPostalCode == y.ShippingPostalCode;
        }

        public int GetHashCode(Order obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.Id);
            hashCode.Add(obj.IsDeleted);
            hashCode.Add(obj.TimeStamp);
            hashCode.Add(obj.CustomerId);
            hashCode.Add((int)obj.Status);
            hashCode.Add(obj.Total);
            hashCode.Add(obj.ShippingCountry);
            hashCode.Add(obj.ShippingCity);
            hashCode.Add(obj.ShippingAddress);
            hashCode.Add(obj.ShippingPostalCode);
            
            return hashCode.ToHashCode();
        }
    }
}