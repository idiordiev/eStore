using eStore.Domain.Entities.Common;

namespace eStore.Domain.Entities.Keyboard
{
    public struct KeyboardSwitch
    {
        public Manufacturer Manufacturer { get; set; }
        public string Name { get; set; }
        public bool IsTactile { get; set; }
        public bool IsClicking { get; set; }
    }
}