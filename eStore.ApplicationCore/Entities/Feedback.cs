using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public class Feedback : Entity
    {
        public Feedback()
        {
            Gamepads = new List<Gamepad>();
        }

        public string Name { get; set; }

        public virtual ICollection<Gamepad> Gamepads { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is Feedback other)
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