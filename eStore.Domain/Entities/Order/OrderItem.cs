using eStore.Domain.Entities.Common;
using eStore.Domain.Shared;

namespace eStore.Domain.Entities.Order
{
    public class OrderItem : Entity
    {
        public Goods Goods { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}