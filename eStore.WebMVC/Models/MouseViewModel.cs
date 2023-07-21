namespace eStore.WebMVC.Models;

public class MouseViewModel : GoodsViewModel
{
    public int ButtonsQuantity { get; set; }
    public string SensorName { get; set; }
    public int MinSensorDPI { get; set; }
    public int MaxSensorDPI { get; set; }
    public string Backlight { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
}