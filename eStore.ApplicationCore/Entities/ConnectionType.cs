using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public class ConnectionType : Entity
    {
        public ConnectionType()
        {
            Goods = new List<Goods>();
        }

        public string Name { get; set; }

        public virtual ICollection<Goods> Goods { get; set; }
    }
}