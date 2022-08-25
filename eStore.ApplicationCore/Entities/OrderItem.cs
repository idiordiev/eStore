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
        
        public override bool Equals(object obj)
        {
            if (obj is OrderItem other)
            {
                return this.Id == other.Id
                       && this.IsDeleted == other.IsDeleted
                       && this.OrderId == other.OrderId
                       && this.UnitPrice == other.UnitPrice
                       && this.GoodsId == other.GoodsId
                       && this.Quantity == other.Quantity;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * IsDeleted.GetHashCode() * OrderId.GetHashCode() * UnitPrice.GetHashCode() 
                       * GoodsId.GetHashCode() * Quantity.GetHashCode();
            }
        }
    }
}