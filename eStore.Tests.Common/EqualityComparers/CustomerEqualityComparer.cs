using System;
using System.Collections.Generic;
using eStore.Domain.Entities;

namespace eStore.Tests.Common.EqualityComparers;

public class CustomerEqualityComparer : IEqualityComparer<Customer>
{
    public bool Equals(Customer x, Customer y)
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
               && x.IdentityId == y.IdentityId
               && x.FirstName == y.FirstName
               && x.LastName == y.LastName
               && x.Email == y.Email
               && x.PhoneNumber == y.PhoneNumber
               && x.Country == y.Country
               && x.City == y.City
               && x.Address == y.Address
               && x.PostalCode == y.PostalCode
               && x.ShoppingCartId == y.ShoppingCartId;
    }

    public int GetHashCode(Customer obj)
    {
        var hashCode = new HashCode();
        hashCode.Add(obj.Id);
        hashCode.Add(obj.IsDeleted);
        hashCode.Add(obj.IdentityId);
        hashCode.Add(obj.FirstName);
        hashCode.Add(obj.LastName);
        hashCode.Add(obj.Email);
        hashCode.Add(obj.PhoneNumber);
        hashCode.Add(obj.Country);
        hashCode.Add(obj.City);
        hashCode.Add(obj.Address);
        hashCode.Add(obj.PostalCode);
        hashCode.Add(obj.ShoppingCartId);

        return hashCode.ToHashCode();
    }
}