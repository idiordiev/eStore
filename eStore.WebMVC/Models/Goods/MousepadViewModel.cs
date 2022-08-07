namespace eStore.WebMVC.Models.Goods
{
    public class MousepadViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ManufacturerName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ConnectionType { get; set; }
        public bool IsStitched { get; set; }
        public string TopMaterial { get; set; }
        public string BottomMaterial { get; set; }
        public string Backlight { get; set; }
        public float Length { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Weight { get; set; }
    }
}