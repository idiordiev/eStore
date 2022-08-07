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
    }
}