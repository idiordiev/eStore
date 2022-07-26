namespace eStore.Domain.Entities.Order
{
    public enum OrderStatus
    {
        New = 1,
        Processing = 2,
        Sent = 3,
        Received = 4,
        Cancelled = 5
    }
}