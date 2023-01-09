using System;
using System.Collections.Generic;
using eStore.Domain.Entities;

namespace eStore.Tests.Common.EqualityComparers
{
    public class KeyboardEqualityComparer : IEqualityComparer<Keyboard>
    {
        public bool Equals(Keyboard x, Keyboard y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null))
            {
                return false;
            }

            if (ReferenceEquals(y, null))
            {
                return false;
            }

            if (x.GetType() != y.GetType())
            {
                return false;
            }

            return x.Id == y.Id
                   && x.IsDeleted == y.IsDeleted
                   && x.Type == y.Type
                   && x.Size == y.Size
                   && x.KeycapMaterial == y.KeycapMaterial
                   && x.FrameMaterial == y.FrameMaterial
                   && x.KeyRollover == y.KeyRollover
                   && x.Backlight == y.Backlight
                   && x.ConnectionType == y.ConnectionType
                   && x.SwitchId == y.SwitchId
                   && x.Length.Equals(y.Length)
                   && x.Width.Equals(y.Width)
                   && x.Height.Equals(y.Height)
                   && x.Weight.Equals(y.Weight);
        }

        public int GetHashCode(Keyboard obj)
        {
            var hashCode = new HashCode();
            hashCode.Add(obj.Id);
            hashCode.Add(obj.IsDeleted);
            hashCode.Add(obj.Type);
            hashCode.Add(obj.Size);
            hashCode.Add(obj.KeycapMaterial);
            hashCode.Add(obj.FrameMaterial);
            hashCode.Add(obj.KeyRollover);
            hashCode.Add(obj.Backlight);
            hashCode.Add(obj.ConnectionType);
            hashCode.Add(obj.SwitchId);
            hashCode.Add(obj.Length);
            hashCode.Add(obj.Width);
            hashCode.Add(obj.Height);
            hashCode.Add(obj.Weight);

            return hashCode.ToHashCode();
        }
    }
}