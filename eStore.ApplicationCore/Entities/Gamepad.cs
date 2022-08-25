using System;
using System.Collections.Generic;

namespace eStore.ApplicationCore.Entities
{
    public class Gamepad : Goods
    {
        public int FeedbackId { get; set; }
        public float Weight { get; set; }
        public virtual Feedback Feedback { get; set; }
        public virtual ICollection<GamepadCompatibleDevice> CompatibleDevices { get; set; }
        
        public override bool Equals(object obj)
        {
            if (obj is Gamepad other)
            {
                return this.Id == other.Id
                       && this.IsDeleted == other.IsDeleted
                       && this.Name == other.Name
                       && this.ManufacturerId == other.ManufacturerId
                       && this.Description == other.Description
                       && this.Price == other.Price
                       && this.ConnectionTypeId == other.ConnectionTypeId
                       && this.ThumbnailImageUrl == other.ThumbnailImageUrl
                       && this.BigImageUrl == other.BigImageUrl
                       && this.Created == other.Created
                       && this.LastModified == other.LastModified
                       && this.FeedbackId == other.FeedbackId
                       && Math.Abs(this.Weight - other.Weight) < 0.01;
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