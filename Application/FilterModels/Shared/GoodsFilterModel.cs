using System.Collections.Generic;

namespace eStore.Application.FilterModels.Shared
{
    public abstract class GoodsFilterModel
    {
        public ICollection<string> Manufacturers { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}