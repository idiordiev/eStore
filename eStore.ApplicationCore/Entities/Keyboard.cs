using eStore.ApplicationCore.Enums;

namespace eStore.ApplicationCore.Entities
{
    public class Keyboard : Goods
    {
        public KeyboardType Type { get; set; }
        public KeyboardSize Size { get; set; }
        public ConnectionType ConnectionType { get; set; }
        public int SwitchId { get; set; }
        public KeyboardMaterial KeycapMaterial { get; set; }
        public KeyboardMaterial FrameMaterial { get; set; }
        public KeyRollover KeyRollover { get; set; }
        public Backlight Backlight { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        
        public KeyboardSwitch Switch { get; set; }
    }
}