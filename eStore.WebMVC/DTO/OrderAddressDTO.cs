using eStore.ApplicationCore.Interfaces.DTO;

namespace eStore.WebMVC.DTO
{
    public class OrderAddressDTO : IOrderAddress
    {
        public string ShippingCity { get; set; }
        public string ShippingAddress { get; set; }
        public string ShippingPostalCode { get; set; }
    }
}