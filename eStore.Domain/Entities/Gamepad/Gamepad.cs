using eStore.Domain.Entities.Common;
using eStore.Domain.Shared;

namespace eStore.Domain.Entities.Gamepad
{
    public class Gamepad : Goods
    {
        public ConnectionType ConnectionType { get; set; }
        public FeedbackType[] Feedback { get; set; }
        public CompatibleType[] CompatibleTypes { get; set; }
        public float Weight { get; set; }
    }
    
}