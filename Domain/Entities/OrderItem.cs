namespace eStore.Domain.Entities
{
    public class OrderItem : Entity
    {
        public int OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public int GoodsId { get; set; }
        public int Quantity { get; set; }

        public Goods Goods { get; set; }
        public Order Order { get; set; }
    }
}