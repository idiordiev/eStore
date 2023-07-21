namespace eStore.WebMVC.Models;

public class OrderItemViewModel
{
    public int GoodsId { get; set; }
    public GoodsViewModel Goods { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}