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
        public float Weight { get; set; }

        public virtual Material TopMaterial { get; set; }
        public virtual Material BottomMaterial { get; set; }
        public virtual Backlight Backlight { get; set; }
    }
}