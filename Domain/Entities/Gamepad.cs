using System;
using System.Collections.Generic;

namespace eStore.Domain.Entities
{
    public class Gamepad : Goods
    {
        public float Weight { get; set; }

        public string ConnectionType { get; set; }
        public string Feedback { get; set; }
        public ICollection<string> CompatibleDevices { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Gamepad other)
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && Name == other.Name
                       && Manufacturer == other.Manufacturer
                       && Description == other.Description
                       && Price == other.Price
                       && ConnectionType == other.ConnectionType
                       && ThumbnailImageUrl == other.ThumbnailImageUrl
                       && BigImageUrl == other.BigImageUrl
                       && Created == other.Created
                       && LastModified == other.LastModified
                       && Feedback== other.Feedback
                       && Math.Abs(Weight - other.Weight) < 0.01;

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * IsDeleted.GetHashCode() * Name.GetHashCode() * Manufacturer.GetHashCode()
                       * Description.GetHashCode() * Price.GetHashCode() * ConnectionType.GetHashCode()
                       * ThumbnailImageUrl.GetHashCode() * BigImageUrl.GetHashCode() * Created.GetHashCode()
                       * LastModified.GetHashCode() * Feedback.GetHashCode() * Weight.GetHashCode();
            }
        }
    }
}