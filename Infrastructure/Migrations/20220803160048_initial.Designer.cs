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
    [Migration("20220803160048_initial")]
    partial class initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("eStore.Application.Entities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AddressLine1")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("AddressLine2")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("City")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Country")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("date");

                    b.Property<string>("Email")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(120)
                        .HasColumnType("nvarchar(120)");

                    b.Property<string>("IdentityId")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

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

                    b.Property<string>("State")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("eStore.Application.Entities.GamepadCompatibleType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CompatibleType")
                        .HasColumnType("int");

                    b.Property<int>("GamepadId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.HasIndex("GamepadId");

                    b.ToTable("GamepadCompatibleWiths");
                });

            modelBuilder.Entity("eStore.Application.Entities.Goods", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Created")
                        .HasColumnType("datetime");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

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

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.ToTable("Goods");
                });

            modelBuilder.Entity("eStore.Application.Entities.KeyboardSwitch", b =>
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

                    b.ToTable("Switches");
                });

            modelBuilder.Entity("eStore.Application.Entities.Manufacturer", b =>
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

            modelBuilder.Entity("eStore.Application.Entities.Order", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CustomerId")
                        .HasColumnType("int");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("ShippingAddressLine1")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ShippingAddressLine2")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ShippingCity")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ShippingCountry")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ShippingPostalCode")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("ShippingState")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

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

            modelBuilder.Entity("eStore.Application.Entities.OrderItem", b =>
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

            modelBuilder.Entity("eStore.Application.Entities.Gamepad", b =>
                {
                    b.HasBaseType("eStore.Application.Entities.Goods");

                    b.Property<int>("ConnectionType")
                        .HasColumnType("int");

                    b.Property<int>("Feedback")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.ToTable("Gamepads");
                });

            modelBuilder.Entity("eStore.Application.Entities.Keyboard", b =>
                {
                    b.HasBaseType("eStore.Application.Entities.Goods");

                    b.Property<int>("Backlight")
                        .HasColumnType("int");

                    b.Property<int>("ConnectionType")
                        .HasColumnType("int");

                    b.Property<int>("FrameMaterial")
                        .HasColumnType("int");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<int>("KeyRollover")
                        .HasColumnType("int");

                    b.Property<int>("KeycapMaterial")
                        .HasColumnType("int");

                    b.Property<float>("Length")
                        .HasColumnType("real");

                    b.Property<int>("Size")
                        .HasColumnType("int");

                    b.Property<int>("SwitchId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.HasIndex("SwitchId");

                    b.ToTable("Keyboards");
                });

            modelBuilder.Entity("eStore.Application.Entities.Mouse", b =>
                {
                    b.HasBaseType("eStore.Application.Entities.Goods");

                    b.Property<int>("Backlight")
                        .HasColumnType("int");

                    b.Property<int>("ButtonsQuantity")
                        .HasColumnType("int");

                    b.Property<int>("ConnectionType")
                        .HasColumnType("int");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<float>("Length")
                        .HasColumnType("real");

                    b.Property<int>("SensorDPI")
                        .HasColumnType("int");

                    b.Property<string>("SensorName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.ToTable("Mouses");
                });

            modelBuilder.Entity("eStore.Application.Entities.Mousepad", b =>
                {
                    b.HasBaseType("eStore.Application.Entities.Goods");

                    b.Property<int>("BottomMaterial")
                        .HasColumnType("int");

                    b.Property<float>("Height")
                        .HasColumnType("real");

                    b.Property<bool>("IsStitched")
                        .HasColumnType("bit");

                    b.Property<float>("Length")
                        .HasColumnType("real");

                    b.Property<int>("TopMaterial")
                        .HasColumnType("int");

                    b.Property<float>("Weight")
                        .HasColumnType("real");

                    b.Property<float>("Width")
                        .HasColumnType("real");

                    b.ToTable("Mousepads");
                });

            modelBuilder.Entity("eStore.Application.Entities.GamepadCompatibleType", b =>
                {
                    b.HasOne("eStore.Application.Entities.Gamepad", "Gamepad")
                        .WithMany("CompatibleWithPlatforms")
                        .HasForeignKey("GamepadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Gamepad");
                });

            modelBuilder.Entity("eStore.Application.Entities.Goods", b =>
                {
                    b.HasOne("eStore.Application.Entities.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("eStore.Application.Entities.KeyboardSwitch", b =>
                {
                    b.HasOne("eStore.Application.Entities.Manufacturer", "Manufacturer")
                        .WithMany()
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("eStore.Application.Entities.Order", b =>
                {
                    b.HasOne("eStore.Application.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Customer");
                });

            modelBuilder.Entity("eStore.Application.Entities.OrderItem", b =>
                {
                    b.HasOne("eStore.Application.Entities.Goods", "Goods")
                        .WithMany("OrderItems")
                        .HasForeignKey("GoodsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("eStore.Application.Entities.Order", "Order")
                        .WithMany("OrderItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Goods");

                    b.Navigation("Order");
                });

            modelBuilder.Entity("eStore.Application.Entities.Gamepad", b =>
                {
                    b.HasOne("eStore.Application.Entities.Goods", null)
                        .WithOne()
                        .HasForeignKey("eStore.Application.Entities.Gamepad", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eStore.Application.Entities.Keyboard", b =>
                {
                    b.HasOne("eStore.Application.Entities.Goods", null)
                        .WithOne()
                        .HasForeignKey("eStore.Application.Entities.Keyboard", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();

                    b.HasOne("eStore.Application.Entities.KeyboardSwitch", "Switch")
                        .WithMany("Keyboards")
                        .HasForeignKey("SwitchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Switch");
                });

            modelBuilder.Entity("eStore.Application.Entities.Mouse", b =>
                {
                    b.HasOne("eStore.Application.Entities.Goods", null)
                        .WithOne()
                        .HasForeignKey("eStore.Application.Entities.Mouse", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eStore.Application.Entities.Mousepad", b =>
                {
                    b.HasOne("eStore.Application.Entities.Goods", null)
                        .WithOne()
                        .HasForeignKey("eStore.Application.Entities.Mousepad", "Id")
                        .OnDelete(DeleteBehavior.ClientCascade)
                        .IsRequired();
                });

            modelBuilder.Entity("eStore.Application.Entities.Customer", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("eStore.Application.Entities.Goods", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("eStore.Application.Entities.KeyboardSwitch", b =>
                {
                    b.Navigation("Keyboards");
                });

            modelBuilder.Entity("eStore.Application.Entities.Order", b =>
                {
                    b.Navigation("OrderItems");
                });

            modelBuilder.Entity("eStore.Application.Entities.Gamepad", b =>
                {
                    b.Navigation("CompatibleWithPlatforms");
                });
#pragma warning restore 612, 618
        }
    }
}
