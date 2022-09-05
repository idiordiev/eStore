using System;

namespace eStore.ApplicationCore.Entities
{
    public class Mousepad : Goods
    {
        public bool IsStitched { get; set; }
        public int TopMaterialId { get; set; }
        public int BottomMaterialId { get; set; }
        public int BacklightId { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }

        public Material TopMaterial { get; set; }
        public Material BottomMaterial { get; set; }
        public Backlight Backlight { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Mousepad other)
            {
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && Name == other.Name
                       && ManufacturerId == other.ManufacturerId
                       && Description == other.Description
                       && Price == other.Price
                       && ThumbnailImageUrl == other.ThumbnailImageUrl
                       && BigImageUrl == other.BigImageUrl
                       && Created == other.Created
                       && LastModified == other.LastModified
                       && IsStitched == other.IsStitched
                       && TopMaterialId == other.TopMaterialId
                       && BottomMaterialId == other.BottomMaterialId
                       && BacklightId == other.BacklightId
                       && Math.Abs(Length - other.Length) < 0.01
                       && Math.Abs(Width - other.Width) < 0.01
                       && Math.Abs(Height - other.Height) < 0.01;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * IsDeleted.GetHashCode() * Name.GetHashCode() * ManufacturerId.GetHashCode()
                       * Description.GetHashCode() * Price.GetHashCode() * ThumbnailImageUrl.GetHashCode()
                       * BigImageUrl.GetHashCode() * Created.GetHashCode() * LastModified.GetHashCode()
                       * IsStitched.GetHashCode() * TopMaterialId.GetHashCode() * BottomMaterialId.GetHashCode()
                       * BacklightId.GetHashCode() * Length.GetHashCode() * Width.GetHashCode() * Height.GetHashCode();
            }
        }
    }
}