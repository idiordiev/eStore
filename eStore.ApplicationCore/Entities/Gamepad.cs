using System;
using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public class Gamepad : Goods
    {
        public int ConnectionTypeId { get; set; }
        public int FeedbackId { get; set; }
        public float Weight { get; set; }

        public ConnectionType ConnectionType { get; set; }
        public Feedback Feedback { get; set; }
        public ICollection<GamepadCompatibleDevice> CompatibleDevices { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Gamepad other)
            {
                return Id == other.Id
                       && IsDeleted == other.IsDeleted
                       && Name == other.Name
                       && ManufacturerId == other.ManufacturerId
                       && Description == other.Description
                       && Price == other.Price
                       && ConnectionTypeId == other.ConnectionTypeId
                       && ThumbnailImageUrl == other.ThumbnailImageUrl
                       && BigImageUrl == other.BigImageUrl
                       && Created == other.Created
                       && LastModified == other.LastModified
                       && FeedbackId == other.FeedbackId
                       && Math.Abs(Weight - other.Weight) < 0.01;
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return Id.GetHashCode() * IsDeleted.GetHashCode() * Name.GetHashCode() * ManufacturerId.GetHashCode()
                       * Description.GetHashCode() * Price.GetHashCode() * ConnectionTypeId.GetHashCode()
                       * ThumbnailImageUrl.GetHashCode() * BigImageUrl.GetHashCode() * Created.GetHashCode()
                       * LastModified.GetHashCode() * FeedbackId.GetHashCode() * Weight.GetHashCode();
            }
        }
    }
}