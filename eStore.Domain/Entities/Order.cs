﻿using System;
using System.Collections.Generic;
using eStore.Domain.Enums;

namespace eStore.Domain.Entities;

public class Order : Entity
{
    public DateTime TimeStamp { get; set; }
    public int CustomerId { get; set; }
    public OrderStatus Status { get; set; }
    public decimal Total { get; set; }
    public string ShippingCountry { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingAddress { get; set; }
    public string ShippingPostalCode { get; set; }

    public Customer Customer { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}