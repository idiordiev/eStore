namespace eStore.WebMVC.Models;

public class GoodsViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string ThumbnailImageUrl { get; set; }
    public string BigImageUrl { get; set; }
    public string ConnectionType { get; set; }
    public bool IsAddedToCart { get; set; }
}