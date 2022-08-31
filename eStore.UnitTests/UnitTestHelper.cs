using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using eStore.ApplicationCore.Entities;
using eStore.ApplicationCore.Enums;
using eStore.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace eStore.UnitTests
{
    internal static class UnitTestHelper
    {
        public static IEnumerable<Backlight> Backlights =>
            new List<Backlight>()
            {
                new() { Id = 1, IsDeleted = false, Name = "Backlight1" },
                new() { Id = 2, IsDeleted = false, Name = "Backlight2" },
                new() { Id = 3, IsDeleted = false, Name = "Backlight3" },
                new() { Id = 4, IsDeleted = false, Name = "Backlight4" }
            };

        public static IEnumerable<Feedback> Feedbacks =>
            new List<Feedback>()
            {
                new() { Id = 1, IsDeleted = false, Name = "Feedback1" },
                new() { Id = 2, IsDeleted = false, Name = "Feedback2" },
                new() { Id = 3, IsDeleted = false, Name = "Feedback3" }
            };

        public static IEnumerable<Manufacturer> Manufacturers =>
            new List<Manufacturer>()
            {
                new() { Id = 1, IsDeleted = false, Name = "Manufacturer1" },
                new() { Id = 2, IsDeleted = false, Name = "Manufacturer2" },
                new() { Id = 3, IsDeleted = false, Name = "Manufacturer3" },
                new() { Id = 4, IsDeleted = false, Name = "Manufacturer4" },
                new() { Id = 5, IsDeleted = false, Name = "Manufacturer5" },
                new() { Id = 6, IsDeleted = false, Name = "Manufacturer6" }
            };

        public static IEnumerable<Material> Materials =>
            new List<Material>()
            {
                new() { Id = 1, IsDeleted = false, Name = "Material1" },
                new() { Id = 2, IsDeleted = false, Name = "Material2" },
                new() { Id = 3, IsDeleted = false, Name = "Material3" },
                new() { Id = 4, IsDeleted = false, Name = "Material4" },
                new() { Id = 5, IsDeleted = false, Name = "Material5" }
            };

        public static IEnumerable<CompatibleDevice> CompatibleDevices =>
            new List<CompatibleDevice>()
            {
                new() { Id = 1, IsDeleted = false, Name = "Device1" },
                new() { Id = 2, IsDeleted = false, Name = "Device2" },
                new() { Id = 3, IsDeleted = false, Name = "Device3" },
                new() { Id = 4, IsDeleted = false, Name = "Device4" }
            };

        public static IEnumerable<ConnectionType> ConnectionTypes =>
            new List<ConnectionType>()
            {
                new() { Id = 1, IsDeleted = false, Name = "ConnectionType1" },
                new() { Id = 2, IsDeleted = false, Name = "ConnectionType2" },
                new() { Id = 3, IsDeleted = false, Name = "ConnectionType3" },
                new() { Id = 4, IsDeleted = false, Name = "ConnectionType4" }
            };

        public static IEnumerable<KeyboardSize> KeyboardSizes =>
            new List<KeyboardSize>()
            {
                new() { Id = 1, IsDeleted = false, Name = "Size1" },
                new() { Id = 2, IsDeleted = false, Name = "Size2" },
                new() { Id = 3, IsDeleted = false, Name = "Size3" }
            };

        public static IEnumerable<KeyboardType> KeyboardTypes =>
            new List<KeyboardType>()
            {
                new() { Id = 1, IsDeleted = false, Name = "Type1" },
                new() { Id = 2, IsDeleted = false, Name = "Type2" },
                new() { Id = 3, IsDeleted = false, Name = "Type3" }
            };

        public static IEnumerable<KeyRollover> KeyRollovers =>
            new List<KeyRollover>()
            {
                new() { Id = 1, IsDeleted = false, Name = "Rollover1" },
                new() { Id = 2, IsDeleted = false, Name = "Rollover2" },
                new() { Id = 3, IsDeleted = false, Name = "Rollover3" }
            };

        public static IEnumerable<KeyboardSwitch> KeyboardSwitches =>
            new List<KeyboardSwitch>()
            {
                new()
                {
                    Id = 1, IsDeleted = false, ManufacturerId = 1, Name = "Switch1", IsClicking = false,
                    IsTactile = false
                },
                new()
                {
                    Id = 2, IsDeleted = false, ManufacturerId = 1, Name = "Switch2", IsClicking = true,
                    IsTactile = false
                },
                new()
                {
                    Id = 3, IsDeleted = false, ManufacturerId = 2, Name = "Switch3", IsClicking = true,
                    IsTactile = true
                },
                new()
                {
                    Id = 4, IsDeleted = false, ManufacturerId = 2, Name = "Switch4", IsClicking = false,
                    IsTactile = true
                },
                new()
                {
                    Id = 5, IsDeleted = false, ManufacturerId = 2, Name = "Switch5", IsClicking = false,
                    IsTactile = false
                }
            };

        public static IEnumerable<Gamepad> Gamepads =>
            new List<Gamepad>()
            {
                new()
                {
                    Id = 1, IsDeleted = false, Name = "Gamepad1", Created = new DateTime(2022, 01, 25, 14, 06, 20),
                    Description = "Description1", ManufacturerId = 3, Price = 34.99m,
                    LastModified = new DateTime(2022, 01, 25, 14, 06, 20),
                    ConnectionTypeId = 1, Weight = 250, FeedbackId = 1, BigImageUrl = "big1.png",
                    ThumbnailImageUrl = "thumbnail1.png", CompatibleDevices = new List<GamepadCompatibleDevice>()
                    {
                        new() { GamepadId = 1, CompatibleDeviceId = 1 },
                        new() { GamepadId = 1, CompatibleDeviceId = 2 },
                        new() { GamepadId = 1, CompatibleDeviceId = 3 }
                    }
                },
                new()
                {
                    Id = 2, IsDeleted = true, Name = "Gamepad2", Created = new DateTime(2022, 01, 25, 14, 07, 20),
                    Description = "Description2", ManufacturerId = 3, Price = 24.99m,
                    LastModified = new DateTime(2022, 01, 25, 14, 07, 20),
                    ConnectionTypeId = 1, Weight = 260, FeedbackId = 1, BigImageUrl = "big2.png",
                    ThumbnailImageUrl = "thumbnail2.png", CompatibleDevices = new List<GamepadCompatibleDevice>()
                    {
                        new() { GamepadId = 2, CompatibleDeviceId = 1 },
                        new() { GamepadId = 2, CompatibleDeviceId = 2 },
                        new() { GamepadId = 2, CompatibleDeviceId = 3 }
                    }
                },
                new()
                {
                    Id = 3, IsDeleted = false, Name = "Gamepad3", Created = new DateTime(2022, 01, 25, 14, 08, 20),
                    Description = "Description2", ManufacturerId = 4, Price = 44.99m,
                    LastModified = new DateTime(2022, 01, 25, 14, 08, 20),
                    ConnectionTypeId = 1, Weight = 220, FeedbackId = 1, BigImageUrl = "big3.png",
                    ThumbnailImageUrl = "thumbnail3.png", CompatibleDevices = new List<GamepadCompatibleDevice>()
                    {
                        new() { GamepadId = 3, CompatibleDeviceId = 1 },
                        new() { GamepadId = 3, CompatibleDeviceId = 2 },
                        new() { GamepadId = 3, CompatibleDeviceId = 3 }
                    }
                },
                new()
                {
                    Id = 4, IsDeleted = false, Name = "Gamepad4", Created = new DateTime(2022, 01, 25, 14, 09, 20),
                    Description = "Description4", ManufacturerId = 5, Price = 54.99m,
                    LastModified = new DateTime(2022, 01, 25, 14, 09, 20),
                    ConnectionTypeId = 2, Weight = 280, FeedbackId = 2, BigImageUrl = "big4.png",
                    ThumbnailImageUrl = "thumbnail4.png", CompatibleDevices = new List<GamepadCompatibleDevice>()
                    {
                        new() { GamepadId = 4, CompatibleDeviceId = 1 },
                        new() { GamepadId = 4, CompatibleDeviceId = 3 }
                    }
                }
            };

        public static IEnumerable<Keyboard> Keyboards =>
            new List<Keyboard>()
            {
                new()
                {
                    Id = 5, IsDeleted = false, Name = "Keyboard5", Created = new DateTime(2022, 01, 25, 14, 10, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 10, 20),
                    Description = "Description5", ManufacturerId = 4, Price = 37.99m, BigImageUrl = "big5.png",
                    ThumbnailImageUrl = "thumbnail5.png",
                    Length = 440, Width = 150, Height = 40, Weight = 700, BacklightId = 1, SizeId = 1, TypeId = 1,
                    ConnectionTypeId = 3, SwitchId = null, FrameMaterialId = 2, KeycapMaterialId = 1,
                    KeyRolloverId = 1
                },
                new()
                {
                    Id = 6, IsDeleted = true, Name = "Keyboard6", Created = new DateTime(2022, 01, 25, 14, 11, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 11, 20),
                    Description = "Description6", ManufacturerId = 4, Price = 47.99m, BigImageUrl = "big6.png",
                    ThumbnailImageUrl = "thumbnail6.png",
                    Length = 380, Width = 140, Height = 30, Weight = 600, BacklightId = 2, SizeId = 1, TypeId = 2,
                    ConnectionTypeId = 3, SwitchId = 1, FrameMaterialId = 2, KeycapMaterialId = 3, KeyRolloverId = 2
                },
                new()
                {
                    Id = 7, IsDeleted = false, Name = "Keyboard7", Created = new DateTime(2022, 01, 25, 14, 12, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 12, 20),
                    Description = "Description7", ManufacturerId = 4, Price = 57.99m, BigImageUrl = "big7.png",
                    ThumbnailImageUrl = "thumbnail7.png",
                    Length = 440, Width = 150, Height = 40, Weight = 750, BacklightId = 3, SizeId = 1, TypeId = 1,
                    ConnectionTypeId = 2, SwitchId = null, FrameMaterialId = 2, KeycapMaterialId = 1,
                    KeyRolloverId = 2
                },
                new()
                {
                    Id = 8, IsDeleted = false, Name = "Keyboard8", Created = new DateTime(2022, 01, 25, 14, 13, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 13, 20),
                    Description = "Description8", ManufacturerId = 5, Price = 53.99m, BigImageUrl = "big8.png",
                    ThumbnailImageUrl = "thumbnail8.png",
                    Length = 450, Width = 140, Height = 40, Weight = 900, BacklightId = 2, SizeId = 2, TypeId = 2,
                    ConnectionTypeId = 1, SwitchId = 2, FrameMaterialId = 1, KeycapMaterialId = 3, KeyRolloverId = 2
                },
                new()
                {
                    Id = 9, IsDeleted = false, Name = "Keyboard9", Created = new DateTime(2022, 01, 25, 14, 14, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 14, 20),
                    Description = "Description9", ManufacturerId = 6, Price = 67.99m, BigImageUrl = "big9.png",
                    ThumbnailImageUrl = "thumbnail9.png",
                    Length = 480, Width = 150, Height = 35, Weight = 1000, BacklightId = 2, SizeId = 2, TypeId = 2,
                    ConnectionTypeId = 3, SwitchId = 3, FrameMaterialId = 1, KeycapMaterialId = 3, KeyRolloverId = 3
                }
            };

        public static IEnumerable<Mouse> Mouses =>
            new List<Mouse>()
            {
                new()
                {
                    Id = 10, IsDeleted = true, Name = "Mouse10", Created = new DateTime(2022, 01, 25, 14, 15, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 15, 20),
                    Description = "Description10", ManufacturerId = 3, Price = 37.99m, BigImageUrl = "big10.png",
                    ThumbnailImageUrl = "thumbnail10.png",
                    Length = 125, Width = 70, Height = 45, Weight = 56,
                    BacklightId = 1, ButtonsQuantity = 4, SensorName = "Sensor1", MinSensorDPI = 100,
                    MaxSensorDPI = 25000, ConnectionTypeId = 1
                },
                new()
                {
                    Id = 11, IsDeleted = false, Name = "Mouse11", Created = new DateTime(2022, 01, 25, 14, 16, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 16, 20),
                    Description = "Description11", ManufacturerId = 4, Price = 47.99m, BigImageUrl = "big11.png",
                    ThumbnailImageUrl = "thumbnail11.png",
                    Length = 127, Width = 63, Height = 52, Weight = 70,
                    BacklightId = 2, ButtonsQuantity = 4, SensorName = "Sensor2", MinSensorDPI = 200,
                    MaxSensorDPI = 20000, ConnectionTypeId = 1
                },
                new()
                {
                    Id = 12, IsDeleted = false, Name = "Mouse12", Created = new DateTime(2022, 01, 25, 14, 17, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 17, 20),
                    Description = "Description12", ManufacturerId = 4, Price = 57.99m, BigImageUrl = "big12.png",
                    ThumbnailImageUrl = "thumbnail12.png",
                    Length = 130, Width = 67, Height = 42, Weight = 83,
                    BacklightId = 2, ButtonsQuantity = 4, SensorName = "Sensor3", MinSensorDPI = 400,
                    MaxSensorDPI = 18000, ConnectionTypeId = 2
                }
            };

        public static IEnumerable<Mousepad> Mousepads =>
            new List<Mousepad>()
            {
                new()
                {
                    Id = 13, IsDeleted = true, Name = "Mousepad13",
                    Created = new DateTime(2022, 01, 25, 14, 18, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 18, 20),
                    Description = "Description13", ManufacturerId = 4, Price = 27.99m, BigImageUrl = "big13.png",
                    ThumbnailImageUrl = "thumbnail13.png",
                    Length = 320, Width = 270, Height = 4, IsStitched = true, BacklightId = 1, BottomMaterialId = 4,
                    TopMaterialId = 5
                },
                new()
                {
                    Id = 14, IsDeleted = false, Name = "Mousepad14",
                    Created = new DateTime(2022, 01, 25, 14, 19, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 19, 20),
                    Description = "Description14", ManufacturerId = 5, Price = 45.99m, BigImageUrl = "big14.png",
                    ThumbnailImageUrl = "thumbnail14.png",
                    Length = 450, Width = 400, Height = 3, IsStitched = true, BacklightId = 1, BottomMaterialId = 4,
                    TopMaterialId = 5
                },
                new()
                {
                    Id = 15, IsDeleted = false, Name = "Mousepad15",
                    Created = new DateTime(2022, 01, 25, 14, 20, 20),
                    LastModified = new DateTime(2022, 01, 25, 14, 20, 20),
                    Description = "Description15", ManufacturerId = 5, Price = 48.99m, BigImageUrl = "big15.png",
                    ThumbnailImageUrl = "thumbnail15.png",
                    Length = 450, Width = 400, Height = 4, IsStitched = true, BacklightId = 1, BottomMaterialId = 2,
                    TopMaterialId = 5
                }
            };
        
        public static IEnumerable<Customer> Customers =>
            new List<Customer>()
            {
                new()
                {
                    Id = 1, IsDeleted = false, Email = "email1@mail.com",
                    IdentityId = "F9168C5E-CEB2-4faa-B6BF-329BF39FA1E4", ShoppingCartId = 1,
                    ShoppingCart = new ShoppingCart() { Id = 1, IsDeleted = false, CustomerId = 1, Goods = new List<GoodsInCart>()
                    {
                        new() { CartId = 1, GoodsId = 1 },
                        new() { CartId = 1, GoodsId = 2 },
                        new() { CartId = 1, GoodsId = 3 },
                        new() { CartId = 1, GoodsId = 4 },
                        new() { CartId = 1, GoodsId = 8 },
                        new() { CartId = 1, GoodsId = 12 }
                    }}
                },
                new()
                {
                    Id = 2, IsDeleted = true, Email = "email2@mail.com",
                    IdentityId = "936DA01F-9ABD-4d9d-80C7-02AF85C822A8", ShoppingCartId = 2,
                    ShoppingCart = new ShoppingCart() { Id = 2, IsDeleted = false, CustomerId = 2, Goods = new List<GoodsInCart>()
                    {
                        
                        new() { CartId = 2, GoodsId = 3 },
                        new() { CartId = 2, GoodsId = 6 },
                        new() { CartId = 2, GoodsId = 2 },
                        new() { CartId = 2, GoodsId = 1 },
                        new() { CartId = 2, GoodsId = 10 },
                        new() { CartId = 2, GoodsId = 12 }
                    }}
                }
            };

        public static IEnumerable<Order> Orders =>
            new List<Order>()
            {
                new()
                {
                    Id = 1, IsDeleted = false, CustomerId = 1, Status = OrderStatus.New,
                    TimeStamp = new DateTime(2022, 02, 10, 13, 45, 23),
                    ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 202.96m,
                    OrderItems = new List<OrderItem>()
                    {
                        new() { Id = 1, IsDeleted = false, OrderId = 1, GoodsId = 1, Quantity = 1, UnitPrice = 34.99m },
                        new() { Id = 2, IsDeleted = false, OrderId = 1, GoodsId = 4, Quantity = 2, UnitPrice = 54.99m },
                        new() { Id = 3, IsDeleted = false, OrderId = 1, GoodsId = 12, Quantity = 1, UnitPrice = 57.99m }
                    }
                },
                new()
                {
                    Id = 2, IsDeleted = false, CustomerId = 1, Status = OrderStatus.Paid,
                    TimeStamp = new DateTime(2022, 02, 10, 13, 45, 23),
                    ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 452.91m,
                    OrderItems = new List<OrderItem>()
                    {
                        new() { Id = 4, IsDeleted = false, OrderId = 2, GoodsId = 3, Quantity = 1, UnitPrice = 44.99m },
                        new() { Id = 5, IsDeleted = false, OrderId = 2, GoodsId = 4, Quantity = 2, UnitPrice = 54.99m },
                        new() { Id = 6, IsDeleted = false, OrderId = 2, GoodsId = 7, Quantity = 2, UnitPrice = 57.99m },
                        new() { Id = 7, IsDeleted = false, OrderId = 2, GoodsId = 5, Quantity = 3, UnitPrice = 37.99m },
                        new() { Id = 8, IsDeleted = false, OrderId = 2, GoodsId = 9, Quantity = 1, UnitPrice = 67.99m }
                    }
                },
                new()
                {
                    Id = 3, IsDeleted = false, CustomerId = 1, Status = OrderStatus.Processing,
                    TimeStamp = new DateTime(2022, 02, 10, 13, 45, 23),
                    ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 286.92m,
                    OrderItems = new List<OrderItem>()
                    {
                        new() { Id = 9, IsDeleted = false, OrderId = 3, GoodsId = 1, Quantity = 4, UnitPrice = 34.99m },
                        new()
                        {
                            Id = 10, IsDeleted = false, OrderId = 3, GoodsId = 5, Quantity = 2, UnitPrice = 37.99m
                        },
                        new()
                        {
                            Id = 11, IsDeleted = false, OrderId = 3, GoodsId = 2, Quantity = 1, UnitPrice = 24.99m
                        },
                        new()
                        {
                            Id = 12, IsDeleted = false, OrderId = 3, GoodsId = 14, Quantity = 1, UnitPrice = 45.99m
                        }
                    }
                },
                new()
                {
                    Id = 4, IsDeleted = false, CustomerId = 1, Status = OrderStatus.Sent,
                    TimeStamp = new DateTime(2022, 02, 10, 13, 45, 23),
                    ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 98.97m,
                    OrderItems = new List<OrderItem>()
                    {
                        new()
                        {
                            Id = 13, IsDeleted = false, OrderId = 4, GoodsId = 15, Quantity = 1, UnitPrice = 48.99m
                        },
                        new()
                        {
                            Id = 14, IsDeleted = false, OrderId = 4, GoodsId = 2, Quantity = 2, UnitPrice = 24.99m
                        }
                    }
                },
                new()
                {
                    Id = 5, IsDeleted = false, CustomerId = 1, Status = OrderStatus.Received,
                    TimeStamp = new DateTime(2022, 02, 10, 13, 46, 23),
                    ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 98.97m,
                    OrderItems = new List<OrderItem>()
                    {
                        new()
                        {
                            Id = 15, IsDeleted = false, OrderId = 5, GoodsId = 15, Quantity = 1, UnitPrice = 48.99m
                        },
                        new()
                        {
                            Id = 16, IsDeleted = false, OrderId = 5, GoodsId = 2, Quantity = 2, UnitPrice = 24.99m
                        }
                    }
                },
                new()
                {
                    Id = 6, IsDeleted = false, CustomerId = 1, Status = OrderStatus.Cancelled,
                    TimeStamp = new DateTime(2022, 02, 10, 13, 47, 23),
                    ShippingAddress = "Address1", ShippingCity = "City1", ShippingPostalCode = "02000", Total = 98.97m,
                    OrderItems = new List<OrderItem>()
                    {
                        new()
                        {
                            Id = 17, IsDeleted = false, OrderId = 6, GoodsId = 15, Quantity = 1, UnitPrice = 48.99m
                        },
                        new()
                        {
                            Id = 18, IsDeleted = false, OrderId = 6, GoodsId = 2, Quantity = 2, UnitPrice = 24.99m
                        }
                    }
                }
            };

        public static IEnumerable<Goods> Goods
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

        public static IEnumerable<OrderItem> OrderItems
        {
            get
            {
                return Orders.SelectMany(o => o.OrderItems);
            }
        }

        public static async Task<ApplicationContext> GetApplicationContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new ApplicationContext(options);

            await context.Backlights.AddRangeAsync(Backlights);
            await context.Feedbacks.AddRangeAsync(Feedbacks);
            await context.Manufacturers.AddRangeAsync(Manufacturers);
            await context.Materials.AddRangeAsync(Materials);
            await context.CompatibleDevices.AddRangeAsync(CompatibleDevices);
            await context.ConnectionTypes.AddRangeAsync(ConnectionTypes);
            await context.KeyboardSizes.AddRangeAsync(KeyboardSizes);
            await context.KeyboardTypes.AddRangeAsync(KeyboardTypes);
            await context.KeyRollovers.AddRangeAsync(KeyRollovers);
            await context.KeyboardSwitches.AddRangeAsync(KeyboardSwitches);
            await context.Customers.AddRangeAsync(Customers);
            await context.Gamepads.AddRangeAsync(Gamepads);
            await context.Keyboards.AddRangeAsync(Keyboards);
            await context.Mouses.AddRangeAsync(Mouses);
            await context.Mousepads.AddRangeAsync(Mousepads);
            await context.Orders.AddRangeAsync(Orders);
            await context.SaveChangesAsync();

            return context;
        }
    }
}