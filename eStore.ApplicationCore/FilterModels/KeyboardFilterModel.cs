using System.Collections.Generic;

namespace eStore.ApplicationCore.FilterModels
{
    public class KeyboardFilterModel : GoodsFilterModel
    {
        public ICollection<int> KeyboardTypeIds { get; set; }
        public ICollection<int> KeyboardSizeIds { get; set; }
        public ICollection<int> SwitchIds { get; set; }
    }
}