using System;

namespace eStore.ApplicationCore.Entities
{
    public class Keyboard : Goods
    {
        public int TypeId { get; set; }
        public int SizeId { get; set; }
        public int? SwitchId { get; set; }
        public int KeycapMaterialId { get; set; }
        public int FrameMaterialId { get; set; }
        public int KeyRolloverId { get; set; }
        public int BacklightId { get; set; }
        public int ConnectionTypeId { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }

        public KeyboardType Type { get; set; }
        public KeyboardSize Size { get; set; }
        public Material KeycapMaterial { get; set; }
        public Material FrameMaterial { get; set; }
        public KeyRollover KeyRollover { get; set; }
        public Backlight Backlight { get; set; }
        public ConnectionType ConnectionType { get; set; }
        public KeyboardSwitch Switch { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Keyboard other)
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && Name == other.Name
                       && ManufacturerId == other.ManufacturerId
                       && Description == other.Description
                       && Price == other.Price
                       && ConnectionTypeId == other.ConnectionTypeId
                       && ThumbnailImageUrl == other.ThumbnailImageUrl
                       && BigImageUrl == other.BigImageUrl
                       && Created == other.Created
                       && LastModified == other.LastModified
                       && TypeId == other.TypeId
                       && SizeId == other.SizeId
                       && SwitchId == other.SwitchId
                       && KeycapMaterialId == other.KeycapMaterialId
                       && FrameMaterialId == other.FrameMaterialId
                       && KeyRolloverId == other.KeyRolloverId
                       && BacklightId == other.BacklightId
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
                return Id.GetHashCode() * IsDeleted.GetHashCode() * Name.GetHashCode() * ManufacturerId.GetHashCode()
                       * Description.GetHashCode() * Price.GetHashCode() * ConnectionTypeId.GetHashCode()
                       * ThumbnailImageUrl.GetHashCode() * BigImageUrl.GetHashCode() * Created.GetHashCode()
                       * LastModified.GetHashCode() * TypeId.GetHashCode() * SizeId.GetHashCode() *
                       SwitchId.GetHashCode()
                       * KeycapMaterialId.GetHashCode() * FrameMaterialId.GetHashCode() * KeyRolloverId.GetHashCode()
                       * BacklightId.GetHashCode() * Length.GetHashCode() * Width.GetHashCode() * Height.GetHashCode() *
                       Weight.GetHashCode();
            }
        }
    }
}