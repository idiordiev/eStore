using eStore.ApplicationCore.Enums;

namespace eStore.ApplicationCore.Entities
{
    public class GamepadCompatibleType : Entity
    {
        public CompatibleType CompatibleType { get; set; }
        public int GamepadId { get; set; }
        
        public Gamepad Gamepad { get; set; }
    }
}