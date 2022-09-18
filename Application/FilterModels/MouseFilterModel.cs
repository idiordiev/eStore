using System.Collections.Generic;

namespace eStore.Application.FilterModels
{
    public class MouseFilterModel : GoodsFilterModel
    {
        public float? MinWeight { get; set; }
        public float? MaxWeight { get; set; }
        public ICollection<int> ConnectionTypeIds { get; set; }
        public ICollection<int> BacklightIds { get; set; }
    }
}