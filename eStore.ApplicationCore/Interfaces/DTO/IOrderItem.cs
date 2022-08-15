namespace eStore.ApplicationCore.Interfaces.DTO
{
    public interface IOrderItem
    {
        int GoodsId { get; set; }
        int Quantity { get; set; }
    }
}