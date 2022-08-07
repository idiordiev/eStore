using System.Collections.Generic;

namespace eStore.ApplicationCore.FilterModels
{
    public abstract class GoodsFilterModel
    {
        public ICollection<int> ManufacturerIds { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}