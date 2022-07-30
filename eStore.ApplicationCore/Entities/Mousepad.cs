using eStore.ApplicationCore.Enums;

namespace eStore.ApplicationCore.Entities
{
    public class Mousepad : Goods
    {
        public bool IsStitched { get; set; }
        public MousepadMaterial TopMaterial { get; set; }
        public MousepadMaterial BottomMaterial { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
    }
}