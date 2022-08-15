using eStore.ApplicationCore.Enums;

namespace eStore.WebMVC.Models.Goods
{
    public class GamepadViewModel : GoodsViewModel
    {
        public string Feedback { get; set; }
        public float Weight { get; set; }
    }
}