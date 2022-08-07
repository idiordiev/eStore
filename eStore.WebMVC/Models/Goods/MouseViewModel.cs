using eStore.ApplicationCore.Enums;

namespace eStore.WebMVC.Models.Goods
{
    public class MouseViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManufacturerName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ConnectionType { get; set; }
        public int ButtonsQuantity { get; set; }
        public string SensorName { get; set; }
        public int SensorDPI { get; set; }
        public string Backlight { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
    }
}