using System.Collections.Generic;
using eStore.Application.FilterModels.Shared;

namespace eStore.Application.FilterModels
{
    public class MouseFilterModel : GoodsFilterModel
    {
        public float? MinWeight { get; set; }
        public float? MaxWeight { get; set; }
        public ICollection<string> ConnectionTypes { get; set; }
        public ICollection<string> Backlights { get; set; }
    }
}