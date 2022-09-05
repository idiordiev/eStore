namespace eStore.ApplicationCore.Entities
{
    public class GoodsInCart
    {
        public int CartId { get; set; }
        public int GoodsId { get; set; }

        public ShoppingCart Cart { get; set; }
        public Goods Goods { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is GoodsInCart other)
            {
                return CartId == other.CartId
                       && GoodsId == other.GoodsId;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return CartId.GetHashCode() * GoodsId.GetHashCode();
            }
        }
    }
}