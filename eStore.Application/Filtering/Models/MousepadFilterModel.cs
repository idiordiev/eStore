using System.Collections.Generic;
using eStore.Application.Filtering.Models.Shared;

namespace eStore.Application.Filtering.Models
{
    public class MousepadFilterModel : GoodsFilterModel
    {
        public ICollection<bool> IsStitchedValues { get; set; }
        public ICollection<string> BottomMaterials { get; set; }
        public ICollection<string> TopMaterials { get; set; }
        public ICollection<string> Backlights { get; set; }
    }
}