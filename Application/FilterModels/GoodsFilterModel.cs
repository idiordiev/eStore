﻿using System.Collections.Generic;

namespace eStore.Application.FilterModels
{
    public abstract class GoodsFilterModel
    {
        public ICollection<int> ManufacturerIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}