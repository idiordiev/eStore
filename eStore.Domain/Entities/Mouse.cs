using System;

namespace eStore.Domain.Entities;

public class Mouse : Goods
{
    public int ButtonsQuantity { get; set; }
    public string SensorName { get; set; }
    public int MinSensorDPI { get; set; }
    public int MaxSensorDPI { get; set; }
    public string ConnectionType { get; set; }
    public string Backlight { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
}