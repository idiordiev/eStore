using eStore.Domain.Entities.Common;

namespace eStore.Domain.Entities.Mouse
{
    public class Mouse : Goods
    {
        public ConnectionType ConnectionType { get; set; }
        public int ButtonsQuantity { get; set; }
        public string SensorName { get; set; }
        public int SensorDPI { get; set; }
        public Backlight Backlight { get; set; }
        public Dimensions Dimensions { get; set; }
        public float Weight { get; set; }
    }
}