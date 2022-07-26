using eStore.Domain.Entities.Common;

namespace eStore.Domain.Entities.Mousepad
{
    public class Mousepad : Goods
    {
        public bool IsStitched { get; set; }
        public MousepadMaterial TopMaterial { get; set; }
        public MousepadMaterial BottomMaterial { get; set; }
        public Dimensions Dimensions { get; set; }
    }
}