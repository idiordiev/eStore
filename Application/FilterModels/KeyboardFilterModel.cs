using System.Collections.Generic;

namespace eStore.Application.FilterModels
{
    public class KeyboardFilterModel : GoodsFilterModel
    {
        public ICollection<int> KeyboardTypeIds { get; set; }
        public ICollection<int> KeyboardSizeIds { get; set; }
        public ICollection<int> ConnectionTypeIds { get; set; }
        public ICollection<int?> SwitchIds { get; set; }
        public ICollection<int> KeyRolloverIds { get; set; }
        public ICollection<int> BacklightIds { get; set; }
    }
}