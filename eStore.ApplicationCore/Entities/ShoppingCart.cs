using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public class ShoppingCart : Entity
    {
        public ShoppingCart()
        {
            Goods = new List<GoodsInCart>();
        }

        public int CustomerId { get; set; }

        public Customer Customer { get; set; }
        public virtual ICollection<GoodsInCart> Goods { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is ShoppingCart other)
            {
                return this.Id == other.Id
                       && this.IsDeleted == other.IsDeleted
                       && this.CustomerId == other.CustomerId;
            }

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