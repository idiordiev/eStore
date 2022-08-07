using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public class Gamepad : Goods
    {
        public int FeedbackId { get; set; }
        public float Weight { get; set; }
        public virtual Feedback Feedback { get; set; }
        public virtual ICollection<GamepadCompatibleDevice> CompatibleDevices { get; set; }
    }
}