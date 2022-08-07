using eStore.ApplicationCore.Enums;

namespace eStore.WebMVC.Models.Goods
{
    public class KeyboardViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManufacturerName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string Size { get; set; }
        public string SwitchName { get; set; }
        public bool SwitchIsTactile { get; set; }
        public bool SwitchIsClicking { get; set; }
        public string ConnectionType { get; set; }
        public string KeycapMaterial { get; set; }
        public string FrameMaterial { get; set; }
        public string KeyRollover { get; set; }
        public string Backlight { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
    }
}