namespace eStore.WebMVC.Models;

public class KeyboardViewModel : GoodsViewModel
{
    public string Type { get; set; }
    public string Size { get; set; }
    public string SwitchName { get; set; }
    public bool SwitchIsTactile { get; set; }
    public bool SwitchIsClicking { get; set; }
    public string KeycapMaterial { get; set; }
    public string FrameMaterial { get; set; }
    public string KeyRollover { get; set; }
    public string Backlight { get; set; }
    public float Length { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public float Weight { get; set; }
}