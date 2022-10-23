using System;
using System.Collections.Generic;
using eStore.Domain.Entities;

namespace eStore.Tests.Common.EqualityComparers
{
    public class GoodsEqualityComparer : IEqualityComparer<Goods>
    {
        public bool Equals(Goods x, Goods y)
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
                   && x.Name == y.Name 
                   && x.Manufacturer == y.Manufacturer 
                   && x.Description == y.Description 
                   && x.Price == y.Price 
                   && x.ThumbnailImageUrl == y.ThumbnailImageUrl 
                   && x.BigImageUrl == y.BigImageUrl 
                   && x.Created.Equals(y.Created) 
                   && x.LastModified.Equals(y.LastModified);
        }

        public int GetHashCode(Goods obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.Id);
            hashCode.Add(obj.IsDeleted);
            hashCode.Add(obj.Name);
            hashCode.Add(obj.Manufacturer);
            hashCode.Add(obj.Description);
            hashCode.Add(obj.Price);
            hashCode.Add(obj.ThumbnailImageUrl);
            hashCode.Add(obj.BigImageUrl);
            hashCode.Add(obj.Created);
            hashCode.Add(obj.LastModified);
            
            return hashCode.ToHashCode();
        }
    }
}