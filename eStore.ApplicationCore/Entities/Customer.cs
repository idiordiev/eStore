using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public class Customer : Entity
    {
        public Customer()
        {
            Orders = new List<Order>();
        }

        public string IdentityId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string PostalCode { get; set; }
        public int ShoppingCartId { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
        public virtual ShoppingCart ShoppingCart { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is Customer other)
            {
                return this.Id == other.Id
                       && this.IsDeleted == other.IsDeleted
                       && this.IdentityId == other.IdentityId
                       && this.FirstName == other.LastName
                       && this.LastName == other.LastName
                       && this.Email == other.Email
                       && this.PhoneNumber == other.PhoneNumber
                       && this.City == other.City
                       && this.Address == other.Address
                       && this.PostalCode == other.PostalCode
                       && this.ShoppingCartId == other.ShoppingCartId;
            }

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