using System;
using System.Collections.Generic;
using eStore.Domain.Entities;

namespace eStore.Tests.Common.EqualityComparers
{
    public class MouseEqualityComparer : IEqualityComparer<Mouse>
    {
        public bool Equals(Mouse x, Mouse y)
        {
            if (ReferenceEquals(x, y)) 
                return true;
            
            if (ReferenceEquals(x, null)) 
                return false;
            
            if (ReferenceEquals(y, null)) 
                return false;
            
            if (x.GetType() != y.GetType()) 
                return false;
            
            return x.Id == y.Id 
                   && x.IsDeleted == y.IsDeleted 
                   && x.ButtonsQuantity == y.ButtonsQuantity 
                   && x.SensorName == y.SensorName 
                   && x.MinSensorDPI == y.MinSensorDPI 
                   && x.MaxSensorDPI == y.MaxSensorDPI 
                   && x.ConnectionType == y.ConnectionType 
                   && x.Backlight == y.Backlight 
                   && x.Length.Equals(y.Length) 
                   && x.Width.Equals(y.Width) 
                   && x.Height.Equals(y.Height) 
                   && x.Weight.Equals(y.Weight);
        }

        public int GetHashCode(Mouse obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.Id);
            hashCode.Add(obj.IsDeleted);
            hashCode.Add(obj.ButtonsQuantity);
            hashCode.Add(obj.SensorName);
            hashCode.Add(obj.MinSensorDPI);
            hashCode.Add(obj.MaxSensorDPI);
            hashCode.Add(obj.ConnectionType);
            hashCode.Add(obj.Backlight);
            hashCode.Add(obj.Length);
            hashCode.Add(obj.Width);
            hashCode.Add(obj.Height);
            hashCode.Add(obj.Weight);
            
            return hashCode.ToHashCode();
        }
    }
}