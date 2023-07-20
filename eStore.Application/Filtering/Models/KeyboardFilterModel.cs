using System.Collections.Generic;
using eStore.Application.Filtering.Models.Shared;

namespace eStore.Application.Filtering.Models
{
    public class KeyboardFilterModel : GoodsFilterModel
    {
        public ICollection<string> KeyboardTypes { get; set; }
        public ICollection<string> KeyboardSizes { get; set; }
        public ICollection<string> ConnectionTypes { get; set; }
        public ICollection<int?> SwitchIds { get; set; }
        public ICollection<string> KeyRollovers { get; set; }
        public ICollection<string> Backlights { get; set; }
    }
}