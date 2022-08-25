namespace eStore.ApplicationCore.Entities
{
    public class GamepadCompatibleDevice
    {
        public int GamepadId { get; set; }
        public int CompatibleDeviceId { get; set; }

        public virtual Gamepad Gamepad { get; set; }
        public virtual CompatibleDevice CompatibleDevice { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is GamepadCompatibleDevice other)
            {
                return this.GamepadId == other.GamepadId
                       && this.CompatibleDeviceId == other.CompatibleDeviceId;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return GamepadId.GetHashCode() * CompatibleDeviceId.GetHashCode();
            }
        }
    }
}