using eStore.Domain.Entities.Common;

namespace eStore.Domain.Entities.Keyboard
{
    public class Keyboard : Goods
    {
        public KeyboardType Type { get; set; }
        public KeyboardSize Size { get; set; }
        public ConnectionType ConnectionType { get; set; }
        public KeyboardSwitch Switch { get; set; }
        public KeyboardMaterial KeycapMaterial { get; set; }
        public KeyboardMaterial FrameMaterial { get; set; }
        public KeyRollover KeyRollover { get; set; }
        public Backlight Backlight { get; set; }
        public Dimensions Dimensions { get; set; }
        public float Weight { get; set; }
    }
}