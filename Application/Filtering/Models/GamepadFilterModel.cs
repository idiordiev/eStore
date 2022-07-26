﻿using System.Collections.Generic;
using eStore.Application.Filtering.Models.Shared;

namespace eStore.Application.Filtering.Models
{
    public class GamepadFilterModel : GoodsFilterModel
    {
        public ICollection<string> ConnectionTypes { get; set; }
        public ICollection<string> Feedbacks { get; set; }
        public ICollection<string> CompatibleDevices { get; set; }
    }
}