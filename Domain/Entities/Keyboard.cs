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

        public override bool Equals(object obj)
        {
            if (obj is Keyboard other)
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && Name == other.Name
                       && Manufacturer == other.Manufacturer
                       && Description == other.Description
                       && Price == other.Price
                       && ConnectionType == other.ConnectionType
                       && ThumbnailImageUrl == other.ThumbnailImageUrl
                       && BigImageUrl == other.BigImageUrl
                       && Created == other.Created
                       && LastModified == other.LastModified
                       && Type == other.Type
                       && Size == other.Size
                       && SwitchId == other.SwitchId
                       && KeycapMaterial == other.KeycapMaterial
                       && FrameMaterial == other.FrameMaterial
                       && KeyRollover == other.KeyRollover
                       && Backlight == other.Backlight
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
                       * Created.GetHashCode() * LastModified.GetHashCode() * SwitchId.GetHashCode()
                       *  Length.GetHashCode() * Width.GetHashCode() * Height.GetHashCode() * Weight.GetHashCode();
            }
        }
    }
}