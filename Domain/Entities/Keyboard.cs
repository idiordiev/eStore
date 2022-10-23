using System;

namespace eStore.Domain.Entities
{
    public class Keyboard : Goods
    {
        public string Type { get; set; }
        public string Size { get; set; }
        public string KeycapMaterial { get; set; }
        public string FrameMaterial { get; set; }
        public string KeyRollover { get; set; }
        public string Backlight { get; set; }
        public string ConnectionType { get; set; }
        public int? SwitchId { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
        public KeyboardSwitch Switch { get; set; }
    }
}