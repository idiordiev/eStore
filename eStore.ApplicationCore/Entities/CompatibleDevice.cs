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

        public ICollection<GamepadCompatibleDevice> Gamepads { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is CompatibleDevice other)
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