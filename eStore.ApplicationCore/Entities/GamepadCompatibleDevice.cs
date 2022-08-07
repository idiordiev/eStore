namespace eStore.ApplicationCore.Entities
{
    public class GamepadCompatibleDevice
    {
        public int GamepadId { get; set; }
        public int CompatibleDeviceId { get; set; }

        public virtual Gamepad Gamepad { get; set; }
        public virtual CompatibleDevice CompatibleDevice { get; set; }
    }
}