using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public class KeyRollover : Entity
    {
        public KeyRollover()
        {
            Keyboards = new List<Keyboard>();
        }

        public string Name { get; set; }

        public virtual ICollection<Keyboard> Keyboards { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is KeyRollover other)
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