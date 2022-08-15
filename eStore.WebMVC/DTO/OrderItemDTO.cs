using eStore.ApplicationCore.Interfaces.DTO;

namespace eStore.WebMVC.DTO
{
    public class OrderItemDTO : IOrderItem
    {
        public int GoodsId { get; set; }
        public int Quantity { get; set; }
    }
}