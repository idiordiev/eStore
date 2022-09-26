﻿using System.Collections.Generic;

namespace eStore.Application.FilterModels
{
    public class MousepadFilterModel : GoodsFilterModel
    {
        public ICollection<bool> IsStitchedValues { get; set; }
        public ICollection<int> BottomMaterialIds { get; set; }
        public ICollection<int> TopMaterialIds { get; set; }
        public ICollection<int> BacklightIds { get; set; }
    }
}