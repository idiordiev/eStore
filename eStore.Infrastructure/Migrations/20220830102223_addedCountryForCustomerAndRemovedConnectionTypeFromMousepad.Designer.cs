﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eStore.Infrastructure.Data;

namespace eStore.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220830102223_addedCountryForCustomerAndRemovedConnectionTypeFromMousepad")]
    partial class addedCountryForCustomerAndRemovedConnectionTypeFromMousepad
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Backlight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Backlights");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.CompatibleDevice", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("CompatibleDevices");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.ConnectionType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("ConnectionTypes");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Address")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Country")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("IdentityId")
                        .HasMaxLength(40)
                        .HasColumnType("nvarchar(40)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("LastName")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("ShoppingCartId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Feedback", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.GamepadCompatibleDevice", b =>
                {
                    b.Property<int>("GamepadId")
                        .HasColumnType("int");

                    b.Property<int>("CompatibleDeviceId")
                        .HasColumnType("int");

                    b.HasKey("GamepadId", "CompatibleDeviceId");

                    b.HasIndex("CompatibleDeviceId");

                    b.ToTable("GamepadCompatibleDevices");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Goods", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("BigImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ThumbnailImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.GoodsInCart", b =>
                {
                    b.Property<int>("CartId")
                        .HasColumnType("int");

                    b.Property<int>("GoodsId")
                        .HasColumnType("int");

                    b.HasKey("CartId", "GoodsId");

                    b.HasIndex("GoodsId");

                    b.ToTable("GoodsInCarts");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.KeyRollover", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("KeyRollovers");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.KeyboardSize", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("KeyboardSizes");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.KeyboardSwitch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsClicking")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("IsTactile")
                        .HasColumnType("bit");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("KeyboardSwitches");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.KeyboardType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("KeyboardTypes");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Material", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Materials");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ShippingAddress")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("ShippingCity")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ShippingCountry")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShippingPostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<DateTime>("TimeStamp")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.OrderItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("GoodsId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("GoodsId");

                    b.HasIndex("OrderId");

                    b.ToTable("OrderItems");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.ShoppingCart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("CustomerId")
                        .IsUnique();

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Gamepad", b =>
                {
                    b.HasBaseType("eStore.ApplicationCore.Entities.Goods");

                    b.Property<int>("ConnectionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("FeedbackId")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.HasIndex("ConnectionTypeId");

                    b.HasIndex("FeedbackId");

                    b.ToTable("Gamepads");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Keyboard", b =>
                {
                    b.HasBaseType("eStore.ApplicationCore.Entities.Goods");

                    b.Property<int>("BacklightId")
                        .HasColumnType("int");

                    b.Property<int>("ConnectionTypeId")
                        .HasColumnType("int");

                    b.Property<int>("FrameMaterialId")
                        .HasColumnType("int");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<int>("KeyRolloverId")
                        .HasColumnType("int");

                    b.Property<int>("KeycapMaterialId")
                        .HasColumnType("int");

                    b.Property<float>("Length")
                        .HasColumnType("real");

                    b.Property<int>("SizeId")
                        .HasColumnType("int");

                    b.Property<int?>("SwitchId")
                        .HasColumnType("int");

                    b.Property<int>("TypeId")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.HasIndex("BacklightId");

                    b.HasIndex("ConnectionTypeId");

                    b.HasIndex("FrameMaterialId");

                    b.HasIndex("KeyRolloverId");

                    b.HasIndex("KeycapMaterialId");

                    b.HasIndex("SizeId");

                    b.HasIndex("SwitchId");

                    b.HasIndex("TypeId");

                    b.ToTable("Keyboards");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Mouse", b =>
                {
                    b.HasBaseType("eStore.ApplicationCore.Entities.Goods");

                    b.Property<int>("BacklightId")
                        .HasColumnType("int");

                    b.Property<int>("ButtonsQuantity")
                        .HasColumnType("int");

                    b.Property<int>("ConnectionTypeId")
                        .HasColumnType("int");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<float>("Length")
                        .HasColumnType("real");

                    b.Property<int>("MaxSensorDPI")
                        .HasColumnType("int");

                    b.Property<int>("MinSensorDPI")
                        .HasColumnType("int");

                    b.Property<string>("SensorName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.HasIndex("BacklightId");

                    b.HasIndex("ConnectionTypeId");

                    b.ToTable("Mouses");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Mousepad", b =>
                {
                    b.HasBaseType("eStore.ApplicationCore.Entities.Goods");

                    b.Property<int>("BacklightId")
                        .HasColumnType("int");

                    b.Property<int>("BottomMaterialId")
                        .HasColumnType("int");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<bool>("IsStitched")
                        .HasColumnType("bit");

                    b.Property<float>("Length")
                        .HasColumnType("real");

                    b.Property<int>("TopMaterialId")
                        .HasColumnType("int");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.HasIndex("BacklightId");

                    b.HasIndex("BottomMaterialId");

                    b.HasIndex("TopMaterialId");

                    b.ToTable("Mousepads");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.GamepadCompatibleDevice", b =>
                {
                    b.HasOne("eStore.ApplicationCore.Entities.CompatibleDevice", "CompatibleDevice")
                        .WithMany("Gamepads")
                        .HasForeignKey("CompatibleDeviceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.Gamepad", "Gamepad")
                        .WithMany("CompatibleDevices")
                        .HasForeignKey("GamepadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("CompatibleDevice");

                    b.Navigation("Gamepad");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Goods", b =>
                {
                    b.HasOne("eStore.ApplicationCore.Entities.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.GoodsInCart", b =>
                {
                    b.HasOne("eStore.ApplicationCore.Entities.ShoppingCart", "Cart")
                        .WithMany("Goods")
                        .HasForeignKey("CartId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.Goods", "Goods")
                        .WithMany("GoodsInCarts")
                        .HasForeignKey("GoodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cart");

                    b.Navigation("Goods");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.KeyboardSwitch", b =>
                {
                    b.HasOne("eStore.ApplicationCore.Entities.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Order", b =>
                {
                    b.HasOne("eStore.ApplicationCore.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.OrderItem", b =>
                {
                    b.HasOne("eStore.ApplicationCore.Entities.Goods", "Goods")
                        .WithMany("OrderItems")
                        .HasForeignKey("GoodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Goods");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.ShoppingCart", b =>
                {
                    b.HasOne("eStore.ApplicationCore.Entities.Customer", "Customer")
                        .WithOne("ShoppingCart")
                        .HasForeignKey("eStore.ApplicationCore.Entities.ShoppingCart", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Gamepad", b =>
                {
                    b.HasOne("eStore.ApplicationCore.Entities.ConnectionType", "ConnectionType")
                        .WithMany()
                        .HasForeignKey("ConnectionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.Feedback", "Feedback")
                        .WithMany("Gamepads")
                        .HasForeignKey("FeedbackId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.Goods", null)
                        .WithOne()
                        .HasForeignKey("eStore.ApplicationCore.Entities.Gamepad", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("ConnectionType");

                    b.Navigation("Feedback");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Keyboard", b =>
                {
                    b.HasOne("eStore.ApplicationCore.Entities.Backlight", "Backlight")
                        .WithMany()
                        .HasForeignKey("BacklightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.ConnectionType", "ConnectionType")
                        .WithMany()
                        .HasForeignKey("ConnectionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.Material", "FrameMaterial")
                        .WithMany()
                        .HasForeignKey("FrameMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.Goods", null)
                        .WithOne()
                        .HasForeignKey("eStore.ApplicationCore.Entities.Keyboard", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.KeyRollover", "KeyRollover")
                        .WithMany("Keyboards")
                        .HasForeignKey("KeyRolloverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.Material", "KeycapMaterial")
                        .WithMany()
                        .HasForeignKey("KeycapMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.KeyboardSize", "Size")
                        .WithMany("Keyboards")
                        .HasForeignKey("SizeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.KeyboardSwitch", "Switch")
                        .WithMany("Keyboards")
                        .HasForeignKey("SwitchId");

                    b.HasOne("eStore.ApplicationCore.Entities.KeyboardType", "Type")
                        .WithMany("Keyboards")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Backlight");

                    b.Navigation("ConnectionType");

                    b.Navigation("FrameMaterial");

                    b.Navigation("KeycapMaterial");

                    b.Navigation("KeyRollover");

                    b.Navigation("Size");

                    b.Navigation("Switch");

                    b.Navigation("Type");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Mouse", b =>
                {
                    b.HasOne("eStore.ApplicationCore.Entities.Backlight", "Backlight")
                        .WithMany()
                        .HasForeignKey("BacklightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.ConnectionType", "ConnectionType")
                        .WithMany()
                        .HasForeignKey("ConnectionTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.Goods", null)
                        .WithOne()
                        .HasForeignKey("eStore.ApplicationCore.Entities.Mouse", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.Navigation("Backlight");

                    b.Navigation("ConnectionType");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Mousepad", b =>
                {
                    b.HasOne("eStore.ApplicationCore.Entities.Backlight", "Backlight")
                        .WithMany()
                        .HasForeignKey("BacklightId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.Material", "BottomMaterial")
                        .WithMany()
                        .HasForeignKey("BottomMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.Goods", null)
                        .WithOne()
                        .HasForeignKey("eStore.ApplicationCore.Entities.Mousepad", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("eStore.ApplicationCore.Entities.Material", "TopMaterial")
                        .WithMany()
                        .HasForeignKey("TopMaterialId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Backlight");

                    b.Navigation("BottomMaterial");

                    b.Navigation("TopMaterial");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.CompatibleDevice", b =>
                {
                    b.Navigation("Gamepads");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Feedback", b =>
                {
                    b.Navigation("Gamepads");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Goods", b =>
                {
                    b.Navigation("GoodsInCarts");

                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.KeyRollover", b =>
                {
                    b.Navigation("Keyboards");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.KeyboardSize", b =>
                {
                    b.Navigation("Keyboards");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.KeyboardSwitch", b =>
                {
                    b.Navigation("Keyboards");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.KeyboardType", b =>
                {
                    b.Navigation("Keyboards");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.ShoppingCart", b =>
                {
                    b.Navigation("Goods");
                });

            modelBuilder.Entity("eStore.ApplicationCore.Entities.Gamepad", b =>
                {
                    b.Navigation("CompatibleDevices");
                });
#pragma warning restore 612, 618
        }
    }
}
