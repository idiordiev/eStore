using System.Collections.Generic;
using eStore.Application.FilterModels.Shared;

namespace eStore.Application.FilterModels
{
    public class GamepadFilterModel : GoodsFilterModel
    {
        public ICollection<int> ConnectionTypeIds { get; set; }
        public ICollection<int> FeedbackIds { get; set; }
        public ICollection<int> CompatibleDevicesIds { get; set; }
    }
}