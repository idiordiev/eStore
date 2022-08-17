namespace eStore.ApplicationCore.FilterModels
{
    public class MouseFilterModel : GoodsFilterModel
    {
        public float? MinWeight { get; set; }
        public float? MaxWeight { get; set; }
    }
}