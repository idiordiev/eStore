using System.Collections.Generic;

namespace eStore.Domain.Entities
{
    public class KeyboardSwitch : Entity
    {
        public string Name { get; set; }
        public string Manufacturer { get; set; }
        public bool IsTactile { get; set; }
        public bool IsClicking { get; set; }
        
        public ICollection<Keyboard> Keyboards { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is KeyboardSwitch other)
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