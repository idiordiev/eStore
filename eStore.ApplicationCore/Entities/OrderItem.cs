using eStore.ApplicationCore.Interfaces.DTO;

namespace eStore.ApplicationCore.Entities
{
    public class OrderItem : Entity, IOrderItem
    {
        public int OrderId { get; set; }
        public decimal UnitPrice { get; set; }

        public Goods Goods { get; set; }
        public Order Order { get; set; }
        public int GoodsId { get; set; }
        public int Quantity { get; set; }
    }
}