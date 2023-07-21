using System.Collections.Generic;

namespace eStore.Domain.Entities;

public class KeyboardSwitch : Entity
{
    public string Name { get; set; }
    public string Manufacturer { get; set; }
    public bool IsTactile { get; set; }
    public bool IsClicking { get; set; }

    public ICollection<Keyboard> Keyboards { get; set; }
}