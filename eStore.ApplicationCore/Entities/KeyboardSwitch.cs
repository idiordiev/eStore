using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public class KeyboardSwitch : Entity
    {
        public KeyboardSwitch()
        {
            Keyboards = new List<Keyboard>();
        }

        public int ManufacturerId { get; set; }
        public string Name { get; set; }
        public bool IsTactile { get; set; }
        public bool IsClicking { get; set; }

        public Manufacturer Manufacturer { get; set; }
        public ICollection<Keyboard> Keyboards { get; set; }
    }
}