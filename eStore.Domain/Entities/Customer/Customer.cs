using eStore.Domain.Entities.Common;
using eStore.Domain.Shared;

namespace eStore.Domain.Entities.Customer
{
    public class Customer : Entity
    {
        public PersonalInfo PersonalInfo { get; set; }
        public Address PersonalAddress { get; set; }
        public ContactInfo ContactInfo { get; set; }

        public Customer()
        {
            PersonalInfo = new PersonalInfo();
            PersonalAddress = new Address();
            ContactInfo = new ContactInfo();
        }
    }
}