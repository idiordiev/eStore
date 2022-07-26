using eStore.Domain.Shared;

namespace eStore.Domain.Entities.Customer
{
    public class ContactInfo : IValueObject
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Telegram { get; set; }
        public string Viber { get; set; }
        public string WhatsApp { get; set; }
        public string FacebookMessenger { get; set; }
        public string Instagram { get; set; }
    }
}