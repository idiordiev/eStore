﻿using System.Collections.Generic;

namespace eStore.Application.FilterModels.Shared
{
    public class GamepadFilterModel : GoodsFilterModel
    {
        public ICollection<int> ConnectionTypeIds { get; set; }
        public ICollection<int> FeedbackIds { get; set; }
        public ICollection<int> CompatibleDevicesIds { get; set; }
    }
}