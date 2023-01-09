using System;
using System.Collections.Generic;
using System.Linq;
using eStore.Domain.Entities;
using eStore.Domain.Enums;
using eStore.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace eStore.Tests.Common
{
    public class UnitTestHelper
    {
        private readonly List<KeyboardSwitch> _keyboardSwitches;
        private readonly List<Gamepad> _gamepads;
        private readonly List<Keyboard> _keyboards;
        private readonly List<Mouse> _mouses;
        private readonly List<Mousepad> _mousepads;
        private readonly List<Customer> _customers;
        private readonly List<Order> _orders;

        public UnitTestHelper()
        {
            _keyboardSwitches = new List<KeyboardSwitch>
            {
                new KeyboardSwitch
                {
                    Id = 1, IsDeleted = false, Manufacturer = "Manufacturer1", Name = "Switch1", IsClicking = false,
                    IsTactile = false
                },
                new KeyboardSwitch
                {
                    Id = 2, IsDeleted = false, Manufacturer = "Manufacturer1", Name = "Switch2", IsClicking = true,
                    IsTactile = false
                },
                new KeyboardSwitch
                {
                    Id = 3, IsDeleted = false, Manufacturer = "Manufacturer2", Name = "Switch3", IsClicking = true,
                    IsTactile = true
                },
                new KeyboardSwitch
                {
                    Id = 4, IsDeleted = false, Manufacturer = "Manufacturer2", Name = "Switch4", IsClicking = false,
                    IsTactile = true
                },
                new KeyboardSwitch
                {
                    Id = 5, IsDeleted = false, Manufacturer = "Manufacturer2", Name = "Switch5", IsClicking = false,
                    IsTactile = false
                }
            };

            _gamepads = new List<Gamepad>
            {
                new Gamepad
                {
                    Id = 1, IsDeleted = false, Name = "Gamepad1", Created = new DateTime(2022, 01, 25, 14, 06, 20),
                    Description = "Description1", Manufacturer = "Manufacturer3", Price = 34.99m,
                    LastModified = new DateTime(2022, 01, 25, 14, 06, 20),
                    ConnectionType = "ConnectionType1", Weight = 250, Feedback = "Feedback1", BigImageUrl = "big1.png",
                    ThumbnailImageUrl = "thumbnail1.png",
                    CompatibleDevices = new List<string>() { "CompatibleDevice1", "CompatibleDevice2" }
                },
                new Gamepad
                {
                    Id = 2, IsDeleted = true, Name = "Gamepad2", Created = new DateTime(2022, 01, 25, 14, 07, 20),
                    Description = "Description2", Manufacturer = "Manufacturer3", Price = 24.99m,
                    LastModified = new DateTime(2022, 01, 25, 14, 07, 20),
                    ConnectionType = "ConnectionType1", Weight = 260, Feedback = "Feedback1", BigImageUrl = "big2.png",
                    ThumbnailImageUrl = "thumbnail2.png",
                    CompatibleDevices = new List<string>() { "CompatibleDevice1", "CompatibleDevice3" }
                },
                new Gamepad
                {
                    Id = 3, IsDeleted = false, Name = "Gamepad3", Created = new DateTime(2022, 01, 25, 14, 08, 20),
                    Description = "Description2", Manufacturer = "Manufacturer4", Price = 44.99m,
                    LastModified = new DateTime(2022, 01, 25, 14, 08, 20),
                    ConnectionType = "ConnectionType1", Weight = 220, Feedback = "Feedback1", BigImageUrl = "big3.png",
                    ThumbnailImageUrl = "thumbnail3.png",
                    CompatibleDevices = new List<string>() { "CompatibleDevice2", "CompatibleDevice3" }
                },
                new Gamepad
                {
                    Id = 4, IsDeleted = false, Name = "Gamepad4", Created = new DateTime(2022, 01, 25, 14, 09, 20),
                    Description = "Description4", Manufacturer = "Manufacturer5", Price = 54.99m,
                    LastModified = new DateTime(2022, 01, 25, 14, 09, 20),
                    ConnectionType = "ConnectionType2", Weight = 280, Feedback = "Feedback2", BigImageUrl = "big4.png",
                    ThumbnailImageUrl = "thumbnail4.png",
                    CompatibleDevices = new List<string>() { "CompatibleDevice1", "CompatibleDevice3" }
                }
            };

            _keyboards = new List<Keyboard>
            {
                new Keyboard
                {
                    Id = 5, IsDeleted = false, Name = "Keyboard5", Created = new DateTime(2022, 01, 25, 14, 10, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 10, 20),
                    Description = "Description5", Manufacturer = "Manufacturer4", Price = 37.99m,
                    BigImageUrl = "big5.png",
                    ThumbnailImageUrl = "thumbnail5.png",
                    Length = 440, Width = 150, Height = 40, Weight = 700, Backlight = "Backlight1", Size = "Size1",
                    Type = "Type1",
                    ConnectionType = "ConnectionType3", SwitchId = null, FrameMaterial = "Material2",
                    KeycapMaterial = "Material1",
                    KeyRollover = "Rollover1"
                },
                new Keyboard
                {
                    Id = 6, IsDeleted = true, Name = "Keyboard6", Created = new DateTime(2022, 01, 25, 14, 11, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 11, 20),
                    Description = "Description6", Manufacturer = "Manufacturer4", Price = 47.99m,
                    BigImageUrl = "big6.png",
                    ThumbnailImageUrl = "thumbnail6.png",
                    Length = 380, Width = 140, Height = 30, Weight = 600, Backlight = "Backlight2", Size = "Size1",
                    Type = "Type2",
                    ConnectionType = "ConnectionType3", SwitchId = 1, FrameMaterial = "Material2",
                    KeycapMaterial = "Material3", KeyRollover = "Rollover2"
                },
                new Keyboard
                {
                    Id = 7, IsDeleted = false, Name = "Keyboard7", Created = new DateTime(2022, 01, 25, 14, 12, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 12, 20),
                    Description = "Description7", Manufacturer = "Manufacturer4", Price = 57.99m,
                    BigImageUrl = "big7.png",
                    ThumbnailImageUrl = "thumbnail7.png",
                    Length = 440, Width = 150, Height = 40, Weight = 750, Backlight = "Backlight3", Size = "Size1",
                    Type = "Type1",
                    ConnectionType = "ConnectionType2", SwitchId = null, FrameMaterial = "Material2",
                    KeycapMaterial = "Material1",
                    KeyRollover = "Rollover2"
                },
                new Keyboard
                {
                    Id = 8, IsDeleted = false, Name = "Keyboard8", Created = new DateTime(2022, 01, 25, 14, 13, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 13, 20),
                    Description = "Description8", Manufacturer = "Manufacturer5", Price = 53.99m,
                    BigImageUrl = "big8.png",
                    ThumbnailImageUrl = "thumbnail8.png",
                    Length = 450, Width = 140, Height = 40, Weight = 900, Backlight = "Backlight2", Size = "Size2",
                    Type = "Type2",
                    ConnectionType = "ConnectionType1", SwitchId = 2, FrameMaterial = "Material1",
                    KeycapMaterial = "Material3", KeyRollover = "Rollover2"
                },
                new Keyboard
                {
                    Id = 9, IsDeleted = false, Name = "Keyboard9", Created = new DateTime(2022, 01, 25, 14, 14, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 14, 20),
                    Description = "Description9", Manufacturer = "Manufacturer6", Price = 67.99m,
                    BigImageUrl = "big9.png",
                    ThumbnailImageUrl = "thumbnail9.png",
                    Length = 480, Width = 150, Height = 35, Weight = 1000, Backlight = "Backlight2", Size = "Size2",
                    Type = "Type2",
                    ConnectionType = "ConnectionType3", SwitchId = 3, FrameMaterial = "Material1",
                    KeycapMaterial = "Material3", KeyRollover = "Rollover3"
                }
            };

            _mouses = new List<Mouse>
            {
                new Mouse
                {
                    Id = 10, IsDeleted = true, Name = "Mouse10", Created = new DateTime(2022, 01, 25, 14, 15, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 15, 20),
                    Description = "Description10", Manufacturer = "Manufacturer3", Price = 37.99m,
                    BigImageUrl = "big10.png",
                    ThumbnailImageUrl = "thumbnail10.png",
                    Length = 125, Width = 70, Height = 45, Weight = 56,
                    Backlight = "Backlight1", ButtonsQuantity = 4, SensorName = "Sensor1", MinSensorDPI = 100,
                    MaxSensorDPI = 25000, ConnectionType = "ConnectionType1"
                },
                new Mouse
                {
                    Id = 11, IsDeleted = false, Name = "Mouse11", Created = new DateTime(2022, 01, 25, 14, 16, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 16, 20),
                    Description = "Description11", Manufacturer = "Manufacturer4", Price = 47.99m,
                    BigImageUrl = "big11.png",
                    ThumbnailImageUrl = "thumbnail11.png",
                    Length = 127, Width = 63, Height = 52, Weight = 70,
                    Backlight = "Backlight2", ButtonsQuantity = 4, SensorName = "Sensor2", MinSensorDPI = 200,
                    MaxSensorDPI = 20000, ConnectionType = "ConnectionType1"
                },
                new Mouse
                {
                    Id = 12, IsDeleted = false, Name = "Mouse12", Created = new DateTime(2022, 01, 25, 14, 17, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 17, 20),
                    Description = "Description12", Manufacturer = "Manufacturer4", Price = 57.99m,
                    BigImageUrl = "big12.png",
                    ThumbnailImageUrl = "thumbnail12.png",
                    Length = 130, Width = 67, Height = 42, Weight = 83,
                    Backlight = "Backlight2", ButtonsQuantity = 4, SensorName = "Sensor3", MinSensorDPI = 400,
                    MaxSensorDPI = 18000, ConnectionType = "ConnectionType2"
                }
            };

            _mousepads = new List<Mousepad>
            {
                new Mousepad
                {
                    Id = 13, IsDeleted = true, Name = "Mousepad13",
                    Created = new DateTime(2022, 01, 25, 14, 18, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 18, 20),
                    Description = "Description13", Manufacturer = "Manufacturer4", Price = 27.99m,
                    BigImageUrl = "big13.png",
                    ThumbnailImageUrl = "thumbnail13.png",
                    Length = 320, Width = 270, Height = 4, IsStitched = true, Backlight = "Backlight1",
                    BottomMaterial = "Material4",
                    TopMaterial = "Material5"
                },
                new Mousepad
                {
                    Id = 14, IsDeleted = false, Name = "Mousepad14",
                    Created = new DateTime(2022, 01, 25, 14, 19, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 19, 20),
                    Description = "Description14", Manufacturer = "Manufacturer5", Price = 45.99m,
                    BigImageUrl = "big14.png",
                    ThumbnailImageUrl = "thumbnail14.png",
                    Length = 450, Width = 400, Height = 3, IsStitched = true, Backlight = "Backlight1",
                    BottomMaterial = "Material4",
                    TopMaterial = "Material5"
                },
                new Mousepad
                {
                    Id = 15, IsDeleted = false, Name = "Mousepad15",
                    Created = new DateTime(2022, 01, 25, 14, 20, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 20, 20),
                    Description = "Description15", Manufacturer = "Manufacturer5", Price = 48.99m,
                    BigImageUrl = "big15.png",
                    ThumbnailImageUrl = "thumbnail15.png",
                    Length = 450, Width = 400, Height = 4, IsStitched = true, Backlight = "Backlight1",
                    BottomMaterial = "Material2",
                    TopMaterial = "Material5"
                }
            };

            _customers = new List<Customer>
            {
                new Customer
                {
                    Id = 1, IsDeleted = false, Email = "email1@mail.com",
                    IdentityId = "F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4", ShoppingCartId = 1,
                    ShoppingCart = new ShoppingCart()
                    {
                        Id = 1, IsDeleted = false, CustomerId = 1,
                        Goods = new List<Goods>(Goods.Where(g => new[] { 1, 2, 3, 4, 8, 12 }.Contains(g.Id)))
                    }
                },
                new Customer
                {
                    Id = 2, IsDeleted = true, Email = "email2@mail.com",
                    IdentityId = "936DA01F-9ABD-4d9d-80C7-02AF85C822A8", ShoppingCartId = 2,
                    ShoppingCart = new ShoppingCart()
                    {
                        Id = 2, IsDeleted = false, CustomerId = 2,
                        Goods = new List<Goods>(Goods.Where(g => new[] { 3, 6, 2, 1, 10, 12 }.Contains(g.Id)))
                    }
                }
            };

            _orders = new List<Order>()
            {
                new Order
                {
                    Id = 1, IsDeleted = false, CustomerId = 1, Status = OrderStatus.New,
                    TimeStamp = new DateTime(2022, 02, 10, 13, 45, 23),
                    ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 202.96m,
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem
                            { Id = 1, IsDeleted = false, OrderId = 1, GoodsId = 1, Quantity = 1, UnitPrice = 34.99m },
                        new OrderItem
                            { Id = 2, IsDeleted = false, OrderId = 1, GoodsId = 4, Quantity = 2, UnitPrice = 54.99m },
                        new OrderItem
                            { Id = 3, IsDeleted = false, OrderId = 1, GoodsId = 12, Quantity = 1, UnitPrice = 57.99m }
                    }
                },
                new Order
                {
                    Id = 2, IsDeleted = false, CustomerId = 1, Status = OrderStatus.Paid,
                    TimeStamp = new DateTime(2022, 02, 10, 13, 45, 23),
                    ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 452.91m,
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem
                            { Id = 4, IsDeleted = false, OrderId = 2, GoodsId = 3, Quantity = 1, UnitPrice = 44.99m },
                        new OrderItem
                            { Id = 5, IsDeleted = false, OrderId = 2, GoodsId = 4, Quantity = 2, UnitPrice = 54.99m },
                        new OrderItem
                            { Id = 6, IsDeleted = false, OrderId = 2, GoodsId = 7, Quantity = 2, UnitPrice = 57.99m },
                        new OrderItem
                            { Id = 7, IsDeleted = false, OrderId = 2, GoodsId = 5, Quantity = 3, UnitPrice = 37.99m },
                        new OrderItem
                            { Id = 8, IsDeleted = false, OrderId = 2, GoodsId = 9, Quantity = 1, UnitPrice = 67.99m }
                    }
                },
                new Order
                {
                    Id = 3, IsDeleted = false, CustomerId = 1, Status = OrderStatus.Processing,
                    TimeStamp = new DateTime(2022, 02, 10, 13, 45, 23),
                    ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 286.92m,
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem
                            { Id = 9, IsDeleted = false, OrderId = 3, GoodsId = 1, Quantity = 4, UnitPrice = 34.99m },
                        new OrderItem
                        {
                            Id = 10, IsDeleted = false, OrderId = 3, GoodsId = 5, Quantity = 2, UnitPrice = 37.99m
                        },
                        new OrderItem
                        {
                            Id = 11, IsDeleted = false, OrderId = 3, GoodsId = 2, Quantity = 1, UnitPrice = 24.99m
                        },
                        new OrderItem
                        {
                            Id = 12, IsDeleted = false, OrderId = 3, GoodsId = 14, Quantity = 1, UnitPrice = 45.99m
                        }
                    }
                },
                new Order
                {
                    Id = 4, IsDeleted = false, CustomerId = 1, Status = OrderStatus.Sent,
                    TimeStamp = new DateTime(2022, 02, 10, 13, 45, 23),
                    ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 98.97m,
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Id = 13, IsDeleted = false, OrderId = 4, GoodsId = 15, Quantity = 1, UnitPrice = 48.99m
                        },
                        new OrderItem
                        {
                            Id = 14, IsDeleted = false, OrderId = 4, GoodsId = 2, Quantity = 2, UnitPrice = 24.99m
                        }
                    }
                },
                new Order
                {
                    Id = 5, IsDeleted = false, CustomerId = 1, Status = OrderStatus.Received,
                    TimeStamp = new DateTime(2022, 02, 10, 13, 46, 23),
                    ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 98.97m,
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Id = 15, IsDeleted = false, OrderId = 5, GoodsId = 15, Quantity = 1, UnitPrice = 48.99m
                        },
                        new OrderItem
                        {
                            Id = 16, IsDeleted = false, OrderId = 5, GoodsId = 2, Quantity = 2, UnitPrice = 24.99m
                        }
                    }
                },
                new Order
                {
                    Id = 6, IsDeleted = false, CustomerId = 1, Status = OrderStatus.Cancelled,
                    TimeStamp = new DateTime(2022, 02, 10, 13, 47, 23),
                    ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 98.97m,
                    OrderItems = new List<OrderItem>()
                    {
                        new OrderItem
                        {
                            Id = 17, IsDeleted = false, OrderId = 6, GoodsId = 15, Quantity = 1, UnitPrice = 48.99m
                        },
                        new OrderItem
                        {
                            Id = 18, IsDeleted = false, OrderId = 6, GoodsId = 2, Quantity = 2, UnitPrice = 24.99m
                        }
                    }
                }
            };
        }

        public IEnumerable<KeyboardSwitch> KeyboardSwitches
        {
            get
            {
                return _keyboardSwitches;
            }
        }

        public IEnumerable<Gamepad> Gamepads
        {
            get
            {
                return _gamepads;
            }
        }


        public IEnumerable<Keyboard> Keyboards
        {
            get
            {
                return _keyboards;
            }
        }

        public IEnumerable<Mouse> Mouses
        {
            get
            {
                return _mouses;
            }
        }

        public IEnumerable<Mousepad> Mousepads
        {
            get
            {
                return _mousepads;
            }
        }

        public IEnumerable<Customer> Customers
        {
            get
            {
                return _customers;
            }
        }

        public IEnumerable<Order> Orders
        {
            get
            {
                return _orders;
            }
        }

        public IEnumerable<Goods> Goods
        {
            get
            {
                var goods = new List<Goods>();
                goods.AddRange(Gamepads);
                goods.AddRange(Keyboards);
                goods.AddRange(Mouses);
                goods.AddRange(Mousepads);
                return goods;
            }
        }

        public IEnumerable<OrderItem> OrderItems
        {
            get
            {
                return Orders.SelectMany(o => o.OrderItems);
            }
        }

        public ApplicationContext GetApplicationContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationContext(options);

            context.KeyboardSwitches.AddRange(_keyboardSwitches);
            context.Gamepads.AddRange(_gamepads);
            context.Keyboards.AddRange(_keyboards);
            context.Mouses.AddRange(_mouses);
            context.Mousepads.AddRange(_mousepads);
            context.Customers.AddRange(_customers);
            context.Orders.AddRange(_orders);
            context.SaveChanges();

            return context;
        }
    }
}