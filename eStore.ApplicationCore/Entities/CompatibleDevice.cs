using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public class CompatibleDevice : Entity
    {
        public CompatibleDevice()
        {
            Gamepads = new List<GamepadCompatibleDevice>();
        }

        public string Name { get; set; }

        public virtual ICollection<GamepadCompatibleDevice> Gamepads { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is CompatibleDevice other)
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