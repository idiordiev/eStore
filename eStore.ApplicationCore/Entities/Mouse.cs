using System;

namespace eStore.ApplicationCore.Entities
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
        
        public override bool Equals(object obj)
        {
            if (obj is Mouse other)
            {
                return this.Id == other.Id
                       && this.IsDeleted == other.IsDeleted
                       && this.Name == other.Name
                       && this.ManufacturerId == other.ManufacturerId
                       && this.Description == other.Description
                       && this.Price == other.Price
                       && this.ConnectionTypeId == other.ConnectionTypeId
                       && this.ThumbnailImageUrl == other.ThumbnailImageUrl
                       && this.BigImageUrl == other.BigImageUrl
                       && this.Created == other.Created
                       && this.LastModified == other.LastModified
                       && this.ButtonsQuantity == other.ButtonsQuantity
                       && this.SensorName == other.SensorName
                       && this.MinSensorDPI == other.MinSensorDPI
                       && this.MaxSensorDPI == other.MaxSensorDPI
                       && this.BacklightId == other.BacklightId
                       && Math.Abs(this.Length - other.Length) < 0.01
                       && Math.Abs(this.Width - other.Width) < 0.01
                       && Math.Abs(this.Height - other.Height) < 0.01
                       && Math.Abs(this.Weight - other.Weight) < 0.01;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * IsDeleted.GetHashCode() * Name.GetHashCode() * ManufacturerId.GetHashCode()
                       * Description.GetHashCode() * Price.GetHashCode() * ConnectionTypeId.GetHashCode()
                       * ThumbnailImageUrl.GetHashCode() * BigImageUrl.GetHashCode() * Created.GetHashCode() 
                       * LastModified.GetHashCode() * ButtonsQuantity.GetHashCode() * SensorName.GetHashCode() 
                       * MaxSensorDPI.GetHashCode() * MinSensorDPI.GetHashCode() * BacklightId.GetHashCode() 
                       * Length.GetHashCode() * Width.GetHashCode() * Height.GetHashCode() * Weight.GetHashCode();
            }
        }
    }
}