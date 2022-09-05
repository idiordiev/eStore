namespace eStore.ApplicationCore.Entities
{
    public class GamepadCompatibleDevice
    {
        public int GamepadId { get; set; }
        public int CompatibleDeviceId { get; set; }

        public Gamepad Gamepad { get; set; }
        public CompatibleDevice CompatibleDevice { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is GamepadCompatibleDevice other)
            {
                return GamepadId == other.GamepadId
                       && CompatibleDeviceId == other.CompatibleDeviceId;
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