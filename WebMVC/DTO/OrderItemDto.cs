using eStore.Application.Interfaces.DTO;

namespace eStore.WebMVC.DTO
{
    public class OrderItemDto : IOrderItem
    {
        public int GoodsId { get; set; }
        public int Quantity { get; set; }
    }
}