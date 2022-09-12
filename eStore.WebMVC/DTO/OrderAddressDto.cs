using eStore.ApplicationCore.Interfaces.DTO;

namespace eStore.WebMVC.DTO
{
    public class OrderAddressDto : IOrderAddress
    {
        public string ShippingCountry { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPostalCode { get; set; }
    }
}