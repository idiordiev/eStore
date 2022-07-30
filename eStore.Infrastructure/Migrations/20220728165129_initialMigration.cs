using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore.Infrastructure.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    IdentityId = table.Column<string>(maxLength: 30, nullable: true),
                    FirstName = table.Column<string>(maxLength: 120, nullable: true),
                    LastName = table.Column<string>(maxLength: 120, nullable: true),
                    DateOfBirth = table.Column<DateTime>(type: "date", nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: true),
                    Country = table.Column<string>(maxLength: 100, nullable: true),
                    State = table.Column<string>(maxLength: 2, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    AddressLine1 = table.Column<string>(maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    TimeStamp = table.Column<DateTime>(nullable: false),
                    CustomerId = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    ShippingCountry = table.Column<string>(maxLength: 100, nullable: true),
                    ShippingState = table.Column<string>(maxLength: 2, nullable: true),
                    ShippingCity = table.Column<string>(maxLength: 100, nullable: true),
                    ShippingAddressLine1 = table.Column<string>(maxLength: 100, nullable: true),
                    ShippingAddressLine2 = table.Column<string>(maxLength: 100, nullable: true),
                    ShippingPostalCode = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Switches",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    ManufacturerId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    IsTactile = table.Column<bool>(nullable: false),
                    IsClicking = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Switches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Switches_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Goods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 150, nullable: true),
                    ManufacturerId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime", nullable: false),
                    LastModified = table.Column<DateTime>(type: "datetime", nullable: false),
                    goods_type = table.Column<string>(nullable: false),
                    ConnectionType = table.Column<int>(nullable: true),
                    Feedback = table.Column<int>(nullable: true),
                    Weight = table.Column<float>(nullable: true),
                    Type = table.Column<int>(nullable: true),
                    Size = table.Column<int>(nullable: true),
                    Keyboard_ConnectionType = table.Column<int>(nullable: true),
                    SwitchId = table.Column<int>(nullable: true),
                    KeycapMaterial = table.Column<int>(nullable: true),
                    FrameMaterial = table.Column<int>(nullable: true),
                    KeyRollover = table.Column<int>(nullable: true),
                    Backlight = table.Column<int>(nullable: true),
                    Length = table.Column<float>(nullable: true),
                    Width = table.Column<float>(nullable: true),
                    Height = table.Column<float>(nullable: true),
                    Keyboard_Weight = table.Column<float>(nullable: true),
                    Mouse_ConnectionType = table.Column<int>(nullable: true),
                    ButtonsQuantity = table.Column<int>(nullable: true),
                    SensorName = table.Column<string>(maxLength: 100, nullable: true),
                    SensorDPI = table.Column<int>(nullable: true),
                    Mouse_Backlight = table.Column<int>(nullable: true),
                    Mouse_Length = table.Column<float>(nullable: true),
                    Mouse_Width = table.Column<float>(nullable: true),
                    Mouse_Height = table.Column<float>(nullable: true),
                    Mouse_Weight = table.Column<float>(nullable: true),
                    IsStitched = table.Column<bool>(nullable: true),
                    TopMaterial = table.Column<int>(nullable: true),
                    BottomMaterial = table.Column<int>(nullable: true),
                    Mousepad_Length = table.Column<float>(nullable: true),
                    Mousepad_Width = table.Column<float>(nullable: true),
                    Mousepad_Height = table.Column<float>(nullable: true),
                    Mousepad_Weight = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goods", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Goods_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Goods_Switches_SwitchId",
                        column: x => x.SwitchId,
                        principalTable: "Switches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "GamepadCompatibleWiths",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    CompatibleType = table.Column<int>(nullable: false),
                    GamepadId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamepadCompatibleWiths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamepadCompatibleWiths_Goods_GamepadId",
                        column: x => x.GamepadId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(nullable: false),
                    OrderId = table.Column<int>(nullable: false),
                    GoodsId = table.Column<int>(nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamepadCompatibleWiths_GamepadId",
                table: "GamepadCompatibleWiths",
                column: "GamepadId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_ManufacturerId",
                table: "Goods",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_SwitchId",
                table: "Goods",
                column: "SwitchId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_GoodsId",
                table: "OrderItems",
                column: "GoodsId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Switches_ManufacturerId",
                table: "Switches",
                column: "ManufacturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GamepadCompatibleWiths");

            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Goods");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Switches");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
