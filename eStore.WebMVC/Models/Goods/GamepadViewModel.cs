using eStore.ApplicationCore.Enums;

namespace eStore.WebMVC.Models.Goods
{
    public class GamepadViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManufacturerName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ConnectionType { get; set; }
        public string Feedback { get; set; }
        public float Weight { get; set; }
    }
}