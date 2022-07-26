using System;
using eStore.Domain.Shared;

namespace eStore.Domain.Entities.Common
{
    public class Goods : Entity
    {
        public string Name { get; set; }
        public Manufacturer Manufacturer { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }
    }
}