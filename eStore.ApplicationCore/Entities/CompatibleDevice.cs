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
    }
}