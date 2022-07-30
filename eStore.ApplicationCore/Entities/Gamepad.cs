using System.Collections.Generic;
using eStore.ApplicationCore.Enums;

namespace eStore.ApplicationCore.Entities
{
    public class Gamepad : Goods
    {
        public ConnectionType ConnectionType { get; set; }
        public GamepadFeedbackType Feedback { get; set; }
        public float Weight { get; set; }
        
        public ICollection<GamepadCompatibleType> CompatibleWithPlatforms { get; set; }
    }
}