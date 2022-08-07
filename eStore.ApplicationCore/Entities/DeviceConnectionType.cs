namespace eStore.ApplicationCore.Entities
{
    public class DeviceConnectionType
    {
        public int GoodsId { get; set; }
        public int ConnectionTypeId { get; set; }

        public virtual Goods Goods { get; set; }
        public virtual ConnectionType ConnectionType { get; set; }
    }
}