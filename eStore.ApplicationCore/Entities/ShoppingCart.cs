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
    }
}