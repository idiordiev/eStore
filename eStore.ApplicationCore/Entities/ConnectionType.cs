using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public class ConnectionType : Entity
    {
        public ConnectionType()
        {
            Goods = new List<DeviceConnectionType>();
        }

        public string Name { get; set; }

        public virtual ICollection<DeviceConnectionType> Goods { get; set; }
    }
}