using System.Collections.Generic;
using eStore.Application.FilterModels.Shared;

namespace eStore.Application.FilterModels
{
    public class GamepadFilterModel : GoodsFilterModel
    {
        public ICollection<string> ConnectionTypes { get; set; }
        public ICollection<string> Feedbacks { get; set; }
        public ICollection<string> CompatibleDevices { get; set; }
    }
}