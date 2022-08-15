namespace eStore.ApplicationCore.FilterModels
{
    public class MouseFilterModel : GoodsFilterModel
    {
        public int? MinWeight { get; set; }
        public int? MaxWeight { get; set; }
    }
}