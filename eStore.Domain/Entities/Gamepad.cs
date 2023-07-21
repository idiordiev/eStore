using System;
using System.Collections.Generic;

namespace eStore.Domain.Entities;

public class Gamepad : Goods
{
    public float Weight { get; set; }

    public string ConnectionType { get; set; }
    public string Feedback { get; set; }
    public ICollection<string> CompatibleDevices { get; set; }
}