using System;

namespace eStore.Domain.Entities
{
    public class Mousepad : Goods
    {
        public bool IsStitched { get; set; }
        public string TopMaterial { get; set; }
        public string BottomMaterial { get; set; }
        public string Backlight { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Mousepad other)
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && Name == other.Name
                       && Manufacturer == other.Manufacturer
                       && Description == other.Description
                       && Price == other.Price
                       && ThumbnailImageUrl == other.ThumbnailImageUrl
                       && BigImageUrl == other.BigImageUrl
                       && Created == other.Created
                       && LastModified == other.LastModified
                       && IsStitched == other.IsStitched
                       && TopMaterial == other.TopMaterial
                       && BottomMaterial == other.BottomMaterial
                       && Backlight == other.Backlight
                       && Math.Abs(Length - other.Length) < 0.01
                       && Math.Abs(Width - other.Width) < 0.01
                       && Math.Abs(Height - other.Height) < 0.01;

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * IsDeleted.GetHashCode() * Name.GetHashCode() * Manufacturer.GetHashCode()
                       * Description.GetHashCode() * Price.GetHashCode() * ThumbnailImageUrl.GetHashCode()
                       * BigImageUrl.GetHashCode() * Created.GetHashCode() * LastModified.GetHashCode()
                       * IsStitched.GetHashCode() * TopMaterial.GetHashCode() * BottomMaterial.GetHashCode()
                       * Backlight.GetHashCode() * Length.GetHashCode() * Width.GetHashCode() * Height.GetHashCode();
            }
        }
    }
}