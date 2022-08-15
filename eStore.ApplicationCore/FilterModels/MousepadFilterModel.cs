using System.Collections.Generic;

namespace eStore.ApplicationCore.FilterModels
{
    public class MousepadFilterModel : GoodsFilterModel
    {
        public ICollection<bool> IsStitchedValues { get; set; }
        public ICollection<int> BottomMaterialIds { get; set; }
        public ICollection<int> TopMaterialIds { get; set; }
    }
}