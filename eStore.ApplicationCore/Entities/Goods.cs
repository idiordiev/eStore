using System;
using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public abstract class Goods : Entity
    {
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
        
        public Manufacturer Manufacturer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }

        public Goods()
        {
            OrderItems = new List<OrderItem>();
        }
    }
}