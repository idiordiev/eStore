namespace eStore.ApplicationCore.Interfaces.DTO
{
    public interface IOrderAddress
    {
        public string ShippingCity { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPostalCode { get; set; }
    }
}