namespace eStore.ApplicationCore.Entities
{
    public class GoodsInCart
    {
        public int CartId { get; set; }
        public int GoodsId { get; set; }

        public virtual ShoppingCart Cart { get; set; }
        public virtual Goods Goods { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is GoodsInCart other)
            {
                return this.CartId == other.CartId
                       && this.GoodsId == other.GoodsId;
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