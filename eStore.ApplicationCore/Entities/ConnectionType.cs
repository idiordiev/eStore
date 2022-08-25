using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public class ConnectionType : Entity
    {
        public ConnectionType()
        {
            Goods = new List<Goods>();
        }

        public string Name { get; set; }

        public virtual ICollection<Goods> Goods { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is ConnectionType other)
            {
                return this.Id == other.Id
                       && this.IsDeleted == other.IsDeleted
                       && this.Name == other.Name;
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