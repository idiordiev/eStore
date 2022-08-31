namespace eStore.ApplicationCore.Interfaces.DTO
{
    public interface IOrderAddress
    {
        public string ShippingCountry { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPostalCode { get; set; }
    }
}