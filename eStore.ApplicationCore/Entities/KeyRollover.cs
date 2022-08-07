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
    }
}