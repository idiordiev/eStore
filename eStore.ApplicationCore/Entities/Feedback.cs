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
    }
}