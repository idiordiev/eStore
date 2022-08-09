namespace eStore.ApplicationCore.Entities
{
    public class GoodsInCart
    {
        public int CartId { get; set; }
        public int GoodsId { get; set; }

        public virtual ShoppingCart Cart { get; set; }
        public virtual Goods Goods { get; set; }
    }
}