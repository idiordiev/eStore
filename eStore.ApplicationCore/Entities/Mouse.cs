﻿namespace eStore.ApplicationCore.Entities
{
    public class Mouse : Goods
    {
        public int ButtonsQuantity { get; set; }
        public string SensorName { get; set; }
        public int MinSensorDPI { get; set; }
        public int MaxSensorDPI { get; set; }
        public int BacklightId { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }

        public virtual Backlight Backlight { get; set; }
    }
}