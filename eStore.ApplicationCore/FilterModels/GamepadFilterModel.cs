using System.Collections.Generic;

namespace eStore.ApplicationCore.FilterModels
{
    public class GamepadFilterModel : GoodsFilterModel
    {
        public ICollection<int> ConnectionTypeIds { get; set; }
        public ICollection<int> FeedbackIds { get; set; }
    }
}