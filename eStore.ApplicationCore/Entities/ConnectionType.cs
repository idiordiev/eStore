using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public class ConnectionType : Entity
    {
        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is ConnectionType other)
            {
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && Name == other.Name;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * Name.GetHashCode() * IsDeleted.GetHashCode();
            }
        }
    }
}