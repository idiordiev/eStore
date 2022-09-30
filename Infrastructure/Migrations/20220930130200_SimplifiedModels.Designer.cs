﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using eStore.Infrastructure.Persistence;

namespace eStore.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20220930130200_SimplifiedModels")]
    partial class SimplifiedModels
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("GoodsShoppingCart", b =>
                {
                    b.Property<int>("GoodsId")
                        .HasColumnType("int");

                    b.Property<int>("ShoppingCartsId")
                        .HasColumnType("int");

                    b.HasKey("GoodsId", "ShoppingCartsId");

                    b.HasIndex("ShoppingCartsId");

                    b.ToTable("GoodsShoppingCart");
                });

            modelBuilder.Entity("eStore.Domain.Entities.Customer", b =>
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
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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

            modelBuilder.Entity("eStore.Domain.Entities.Goods", b =>
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

                    b.Property<string>("Manufacturer")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("ThumbnailImageUrl")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("eStore.Domain.Entities.KeyboardSwitch", b =>
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

                    b.Property<string>("Manufacturer")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id");

                    b.ToTable("KeyboardSwitches");
                });

            modelBuilder.Entity("eStore.Domain.Entities.Order", b =>
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
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

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

            modelBuilder.Entity("eStore.Domain.Entities.OrderItem", b =>
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

            modelBuilder.Entity("eStore.Domain.Entities.ShoppingCart", b =>
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

            modelBuilder.Entity("eStore.Domain.Entities.Gamepad", b =>
                {
                    b.HasBaseType("eStore.Domain.Entities.Goods");

                    b.Property<string>("CompatibleDevices")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ConnectionType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Feedback")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.ToTable("Gamepads");
                });

            modelBuilder.Entity("eStore.Domain.Entities.Keyboard", b =>
                {
                    b.HasBaseType("eStore.Domain.Entities.Goods");

                    b.Property<string>("Backlight")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("ConnectionType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FrameMaterial")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<string>("KeyRollover")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("KeycapMaterial")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Length")
                        .HasColumnType("real");

                    b.Property<string>("Size")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int?>("SwitchId")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.HasIndex("SwitchId");

                    b.ToTable("Keyboards");
                });

            modelBuilder.Entity("eStore.Domain.Entities.Mouse", b =>
                {
                    b.HasBaseType("eStore.Domain.Entities.Goods");

                    b.Property<string>("Backlight")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ButtonsQuantity")
                        .HasColumnType("int");

                    b.Property<string>("ConnectionType")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

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

                    b.ToTable("Mouses");
                });

            modelBuilder.Entity("eStore.Domain.Entities.Mousepad", b =>
                {
                    b.HasBaseType("eStore.Domain.Entities.Goods");

                    b.Property<string>("Backlight")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("BottomMaterial")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<bool>("IsStitched")
                        .HasColumnType("bit");

                    b.Property<float>("Length")
                        .HasColumnType("real");

                    b.Property<string>("TopMaterial")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.ToTable("Mousepads");
                });

            modelBuilder.Entity("GoodsShoppingCart", b =>
                {
                    b.HasOne("eStore.Domain.Entities.Goods", null)
                        .WithMany()
                        .HasForeignKey("GoodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.Domain.Entities.ShoppingCart", null)
                        .WithMany()
                        .HasForeignKey("ShoppingCartsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eStore.Domain.Entities.Order", b =>
                {
                    b.HasOne("eStore.Domain.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("eStore.Domain.Entities.OrderItem", b =>
                {
                    b.HasOne("eStore.Domain.Entities.Goods", "Goods")
                        .WithMany("OrderItems")
                        .HasForeignKey("GoodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.Domain.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Goods");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("eStore.Domain.Entities.ShoppingCart", b =>
                {
                    b.HasOne("eStore.Domain.Entities.Customer", "Customer")
                        .WithOne("ShoppingCart")
                        .HasForeignKey("eStore.Domain.Entities.ShoppingCart", "CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("eStore.Domain.Entities.Gamepad", b =>
                {
                    b.HasOne("eStore.Domain.Entities.Goods", null)
                        .WithOne()
                        .HasForeignKey("eStore.Domain.Entities.Gamepad", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eStore.Domain.Entities.Keyboard", b =>
                {
                    b.HasOne("eStore.Domain.Entities.Goods", null)
                        .WithOne()
                        .HasForeignKey("eStore.Domain.Entities.Keyboard", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("eStore.Domain.Entities.KeyboardSwitch", "Switch")
                        .WithMany("Keyboards")
                        .HasForeignKey("SwitchId");

                    b.Navigation("Switch");
                });

            modelBuilder.Entity("eStore.Domain.Entities.Mouse", b =>
                {
                    b.HasOne("eStore.Domain.Entities.Goods", null)
                        .WithOne()
                        .HasForeignKey("eStore.Domain.Entities.Mouse", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eStore.Domain.Entities.Mousepad", b =>
                {
                    b.HasOne("eStore.Domain.Entities.Goods", null)
                        .WithOne()
                        .HasForeignKey("eStore.Domain.Entities.Mousepad", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eStore.Domain.Entities.Customer", b =>
                {
                    b.Navigation("Orders");

                    b.Navigation("ShoppingCart");
                });

            modelBuilder.Entity("eStore.Domain.Entities.Goods", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("eStore.Domain.Entities.KeyboardSwitch", b =>
                {
                    b.Navigation("Keyboards");
                });

            modelBuilder.Entity("eStore.Domain.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });
#pragma warning restore 612, 618
        }
    }
}
