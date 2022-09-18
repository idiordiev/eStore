using System.Collections.Generic;

namespace eStore.Domain.Entities
{
    public class Feedback : Entity
    {
        public Feedback()
        {
            Gamepads = new List<Gamepad>();
        }

        public string Name { get; set; }

        public ICollection<Gamepad> Gamepads { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Feedback other)
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && Name == other.Name;

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