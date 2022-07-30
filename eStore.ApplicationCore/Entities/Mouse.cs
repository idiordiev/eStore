using eStore.ApplicationCore.Enums;

namespace eStore.ApplicationCore.Entities
{
    public class Mouse : Goods
    {
        public ConnectionType ConnectionType { get; set; }
        public int ButtonsQuantity { get; set; }
        public string SensorName { get; set; }
        public int SensorDPI { get; set; }
        public Backlight Backlight { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
    }
}