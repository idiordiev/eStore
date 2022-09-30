using System.Collections.Generic;

namespace eStore.Domain.Entities
{
    public class Customer : Entity
    {
        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public int ShoppingCartId { get; set; }

        public ICollection<Order> Orders { get; set; }
        public ShoppingCart ShoppingCart { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Customer other)
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && IdentityId == other.IdentityId
                       && FirstName == other.LastName
                       && LastName == other.LastName
                       && Email == other.Email
                       && PhoneNumber == other.PhoneNumber
                       && Country == other.Country
                       && City == other.City
                       && Address == other.Address
                       && PostalCode == other.PostalCode
                       && ShoppingCartId == other.ShoppingCartId;

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * IsDeleted.GetHashCode() * IdentityId.GetHashCode() * FirstName.GetHashCode()
                       * LastName.GetHashCode() * Email.GetHashCode() * PhoneNumber.GetHashCode() * City.GetHashCode()
                       * Address.GetHashCode() * PostalCode.GetHashCode() * ShoppingCartId.GetHashCode();
            }
        }
    }
}