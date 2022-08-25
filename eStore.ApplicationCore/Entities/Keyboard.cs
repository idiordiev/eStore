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
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }

        public virtual KeyboardType Type { get; set; }
        public virtual KeyboardSize Size { get; set; }
        public virtual Material KeycapMaterial { get; set; }
        public virtual Material FrameMaterial { get; set; }
        public virtual KeyRollover KeyRollover { get; set; }
        public virtual Backlight Backlight { get; set; }
        public virtual KeyboardSwitch Switch { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is Keyboard other)
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
                       && this.TypeId == other.TypeId
                       && this.SizeId == other.SizeId
                       && this.SwitchId == other.SwitchId
                       && this.KeycapMaterialId == other.KeycapMaterialId
                       && this.FrameMaterialId == other.FrameMaterialId
                       && this.KeyRolloverId == other.KeyRolloverId
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
                       * LastModified.GetHashCode() * TypeId.GetHashCode() * SizeId.GetHashCode() * SwitchId.GetHashCode() 
                       * KeycapMaterialId.GetHashCode() * FrameMaterialId.GetHashCode() * KeyRolloverId.GetHashCode() 
                       * BacklightId.GetHashCode() * Length.GetHashCode() * Width.GetHashCode() * Height.GetHashCode() * Weight.GetHashCode();
            }
        }
    }
}