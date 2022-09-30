using System;

namespace eStore.Domain.Entities
{
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
        
        public override bool Equals(object obj)
        {
            if (obj is Mouse other)
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && Name == other.Name
                       && Description == other.Description
                       && Price == other.Price
                       && ThumbnailImageUrl == other.ThumbnailImageUrl
                       && BigImageUrl == other.BigImageUrl
                       && Created == other.Created
                       && LastModified == other.LastModified
                       && ButtonsQuantity == other.ButtonsQuantity
                       && SensorName == other.SensorName
                       && MinSensorDPI == other.MinSensorDPI
                       && MaxSensorDPI == other.MaxSensorDPI
                       && Math.Abs(Length - other.Length) < 0.01
                       && Math.Abs(Width - other.Width) < 0.01
                       && Math.Abs(Height - other.Height) < 0.01
                       && Math.Abs(Weight - other.Weight) < 0.01;

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * IsDeleted.GetHashCode() * Name.GetHashCode() * Description.GetHashCode() 
                       * Price.GetHashCode() * ThumbnailImageUrl.GetHashCode() * BigImageUrl.GetHashCode() 
                       * Created.GetHashCode() * LastModified.GetHashCode() * ButtonsQuantity.GetHashCode() 
                       * SensorName.GetHashCode() * MaxSensorDPI.GetHashCode() * MinSensorDPI.GetHashCode()
                       * Length.GetHashCode() * Width.GetHashCode() * Height.GetHashCode() * Weight.GetHashCode();
            }
        }
    }
}