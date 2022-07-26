using eStore.Domain.Shared;

namespace eStore.Domain.Entities.Common
{
    public class Address : IValueObject
    {
        public string Country { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string PostalCode { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Address other)
                return Country.Equals(other.Country)
                       && State.Equals(other.State)
                       && City.Equals(other.City)
                       && AddressLine1.Equals(other.AddressLine1)
                       && AddressLine2.Equals(other.AddressLine2)
                       && PostalCode.Equals(other.PostalCode);

            return false;
        }

        public static bool operator ==(Address left, Address right)
        {
            if (left == null && right == null)
                return true;
            if (left == null || right == null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Address left, Address right)
        {
            return !(left == right);
        }
    }
}