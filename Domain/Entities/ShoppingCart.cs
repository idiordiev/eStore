using System.Collections.Generic;

namespace eStore.Domain.Entities
{
    public class ShoppingCart : Entity
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public ICollection<Goods> Goods { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ShoppingCart other)
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && CustomerId == other.CustomerId;

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * CustomerId.GetHashCode() * IsDeleted.GetHashCode();
            }
        }
    }
}