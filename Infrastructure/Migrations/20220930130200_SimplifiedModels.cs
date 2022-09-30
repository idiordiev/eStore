using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore.Infrastructure.Migrations
{
    public partial class SimplifiedModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gamepads_ConnectionTypes_ConnectionTypeId",
                table: "Gamepads");

            migrationBuilder.DropForeignKey(
                name: "FK_Gamepads_Feedbacks_FeedbackId",
                table: "Gamepads");

            migrationBuilder.DropForeignKey(
                name: "FK_Goods_Manufacturers_ManufacturerId",
                table: "Goods");

            migrationBuilder.DropForeignKey(
                name: "FK_Keyboards_Backlights_BacklightId",
                table: "Keyboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Keyboards_ConnectionTypes_ConnectionTypeId",
                table: "Keyboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Keyboards_KeyboardSizes_SizeId",
                table: "Keyboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Keyboards_KeyboardTypes_TypeId",
                table: "Keyboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Keyboards_KeyRollovers_KeyRolloverId",
                table: "Keyboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Keyboards_Materials_FrameMaterialId",
                table: "Keyboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Keyboards_Materials_KeycapMaterialId",
                table: "Keyboards");

            migrationBuilder.DropForeignKey(
                name: "FK_KeyboardSwitches_Manufacturers_ManufacturerId",
                table: "KeyboardSwitches");

            migrationBuilder.DropForeignKey(
                name: "FK_Mousepads_Backlights_BacklightId",
                table: "Mousepads");

            migrationBuilder.DropForeignKey(
                name: "FK_Mousepads_Materials_BottomMaterialId",
                table: "Mousepads");

            migrationBuilder.DropForeignKey(
                name: "FK_Mousepads_Materials_TopMaterialId",
                table: "Mousepads");

            migrationBuilder.DropForeignKey(
                name: "FK_Mouses_Backlights_BacklightId",
                table: "Mouses");

            migrationBuilder.DropForeignKey(
                name: "FK_Mouses_ConnectionTypes_ConnectionTypeId",
                table: "Mouses");

            migrationBuilder.DropTable(
                name: "Backlights");

            migrationBuilder.DropTable(
                name: "ConnectionTypes");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "GamepadCompatibleDevices");

            migrationBuilder.DropTable(
                name: "GoodsInCarts");

            migrationBuilder.DropTable(
                name: "KeyboardSizes");

            migrationBuilder.DropTable(
                name: "KeyboardTypes");

            migrationBuilder.DropTable(
                name: "KeyRollovers");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "CompatibleDevices");

            migrationBuilder.DropIndex(
                name: "IX_Mouses_BacklightId",
                table: "Mouses");

            migrationBuilder.DropIndex(
                name: "IX_Mouses_ConnectionTypeId",
                table: "Mouses");

            migrationBuilder.DropIndex(
                name: "IX_Mousepads_BacklightId",
                table: "Mousepads");

            migrationBuilder.DropIndex(
                name: "IX_Mousepads_BottomMaterialId",
                table: "Mousepads");

            migrationBuilder.DropIndex(
                name: "IX_Mousepads_TopMaterialId",
                table: "Mousepads");

            migrationBuilder.DropIndex(
                name: "IX_KeyboardSwitches_ManufacturerId",
                table: "KeyboardSwitches");

            migrationBuilder.DropIndex(
                name: "IX_Keyboards_BacklightId",
                table: "Keyboards");

            migrationBuilder.DropIndex(
                name: "IX_Keyboards_ConnectionTypeId",
                table: "Keyboards");

            migrationBuilder.DropIndex(
                name: "IX_Keyboards_FrameMaterialId",
                table: "Keyboards");

            migrationBuilder.DropIndex(
                name: "IX_Keyboards_KeycapMaterialId",
                table: "Keyboards");

            migrationBuilder.DropIndex(
                name: "IX_Keyboards_KeyRolloverId",
                table: "Keyboards");

            migrationBuilder.DropIndex(
                name: "IX_Keyboards_SizeId",
                table: "Keyboards");

            migrationBuilder.DropIndex(
                name: "IX_Keyboards_TypeId",
                table: "Keyboards");

            migrationBuilder.DropIndex(
                name: "IX_Goods_ManufacturerId",
                table: "Goods");

            migrationBuilder.DropIndex(
                name: "IX_Gamepads_ConnectionTypeId",
                table: "Gamepads");

            migrationBuilder.DropIndex(
                name: "IX_Gamepads_FeedbackId",
                table: "Gamepads");

            migrationBuilder.DropColumn(
                name: "BacklightId",
                table: "Mouses");

            migrationBuilder.DropColumn(
                name: "ConnectionTypeId",
                table: "Mouses");

            migrationBuilder.DropColumn(
                name: "BacklightId",
                table: "Mousepads");

            migrationBuilder.DropColumn(
                name: "BottomMaterialId",
                table: "Mousepads");

            migrationBuilder.DropColumn(
                name: "TopMaterialId",
                table: "Mousepads");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "KeyboardSwitches");

            migrationBuilder.DropColumn(
                name: "BacklightId",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "ConnectionTypeId",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "FrameMaterialId",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "KeyRolloverId",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "KeycapMaterialId",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "SizeId",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "ManufacturerId",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "ConnectionTypeId",
                table: "Gamepads");

            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "Gamepads");

            migrationBuilder.AddColumn<string>(
                name: "Backlight",
                table: "Mouses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConnectionType",
                table: "Mouses",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Backlight",
                table: "Mousepads",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BottomMaterial",
                table: "Mousepads",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TopMaterial",
                table: "Mousepads",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "KeyboardSwitches",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Backlight",
                table: "Keyboards",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConnectionType",
                table: "Keyboards",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FrameMaterial",
                table: "Keyboards",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeyRollover",
                table: "Keyboards",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "KeycapMaterial",
                table: "Keyboards",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Size",
                table: "Keyboards",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "Keyboards",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Manufacturer",
                table: "Goods",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CompatibleDevices",
                table: "Gamepads",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConnectionType",
                table: "Gamepads",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Feedback",
                table: "Gamepads",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.CreateTable(
                name: "GoodsShoppingCart",
                columns: table => new
                {
                    GoodsId = table.Column<int>(type: "int", nullable: false),
                    ShoppingCartsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsShoppingCart", x => new { x.GoodsId, x.ShoppingCartsId });
                    table.ForeignKey(
                        name: "FK_GoodsShoppingCart_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsShoppingCart_ShoppingCarts_ShoppingCartsId",
                        column: x => x.ShoppingCartsId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GoodsShoppingCart_ShoppingCartsId",
                table: "GoodsShoppingCart",
                column: "ShoppingCartsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoodsShoppingCart");

            migrationBuilder.DropColumn(
                name: "Backlight",
                table: "Mouses");

            migrationBuilder.DropColumn(
                name: "ConnectionType",
                table: "Mouses");

            migrationBuilder.DropColumn(
                name: "Backlight",
                table: "Mousepads");

            migrationBuilder.DropColumn(
                name: "BottomMaterial",
                table: "Mousepads");

            migrationBuilder.DropColumn(
                name: "TopMaterial",
                table: "Mousepads");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "KeyboardSwitches");

            migrationBuilder.DropColumn(
                name: "Backlight",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "ConnectionType",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "FrameMaterial",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "KeyRollover",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "KeycapMaterial",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "Size",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "Manufacturer",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "CompatibleDevices",
                table: "Gamepads");

            migrationBuilder.DropColumn(
                name: "ConnectionType",
                table: "Gamepads");

            migrationBuilder.DropColumn(
                name: "Feedback",
                table: "Gamepads");

            migrationBuilder.AddColumn<int>(
                name: "BacklightId",
                table: "Mouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConnectionTypeId",
                table: "Mouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BacklightId",
                table: "Mousepads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BottomMaterialId",
                table: "Mousepads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TopMaterialId",
                table: "Mousepads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerId",
                table: "KeyboardSwitches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BacklightId",
                table: "Keyboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConnectionTypeId",
                table: "Keyboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FrameMaterialId",
                table: "Keyboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KeyRolloverId",
                table: "Keyboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "KeycapMaterialId",
                table: "Keyboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SizeId",
                table: "Keyboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "Keyboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ManufacturerId",
                table: "Goods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConnectionTypeId",
                table: "Gamepads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "FeedbackId",
                table: "Gamepads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Backlights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backlights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompatibleDevices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompatibleDevices", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Feedbacks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GoodsInCarts",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false),
                    GoodsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoodsInCarts", x => new { x.CartId, x.GoodsId });
                    table.ForeignKey(
                        name: "FK_GoodsInCarts_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GoodsInCarts_ShoppingCarts_CartId",
                        column: x => x.CartId,
                        principalTable: "ShoppingCarts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "KeyboardSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyboardSizes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyboardTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyboardTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyRollovers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyRollovers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Materials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GamepadCompatibleDevices",
                columns: table => new
                {
                    GamepadId = table.Column<int>(type: "int", nullable: false),
                    CompatibleDeviceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamepadCompatibleDevices", x => new { x.GamepadId, x.CompatibleDeviceId });
                    table.ForeignKey(
                        name: "FK_GamepadCompatibleDevices_CompatibleDevices_CompatibleDeviceId",
                        column: x => x.CompatibleDeviceId,
                        principalTable: "CompatibleDevices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamepadCompatibleDevices_Gamepads_GamepadId",
                        column: x => x.GamepadId,
                        principalTable: "Gamepads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mouses_BacklightId",
                table: "Mouses",
                column: "BacklightId");

            migrationBuilder.CreateIndex(
                name: "IX_Mouses_ConnectionTypeId",
                table: "Mouses",
                column: "ConnectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Mousepads_BacklightId",
                table: "Mousepads",
                column: "BacklightId");

            migrationBuilder.CreateIndex(
                name: "IX_Mousepads_BottomMaterialId",
                table: "Mousepads",
                column: "BottomMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Mousepads_TopMaterialId",
                table: "Mousepads",
                column: "TopMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_KeyboardSwitches_ManufacturerId",
                table: "KeyboardSwitches",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_BacklightId",
                table: "Keyboards",
                column: "BacklightId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_ConnectionTypeId",
                table: "Keyboards",
                column: "ConnectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_FrameMaterialId",
                table: "Keyboards",
                column: "FrameMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_KeycapMaterialId",
                table: "Keyboards",
                column: "KeycapMaterialId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_KeyRolloverId",
                table: "Keyboards",
                column: "KeyRolloverId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_SizeId",
                table: "Keyboards",
                column: "SizeId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_TypeId",
                table: "Keyboards",
                column: "TypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_ManufacturerId",
                table: "Goods",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamepads_ConnectionTypeId",
                table: "Gamepads",
                column: "ConnectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamepads_FeedbackId",
                table: "Gamepads",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_GamepadCompatibleDevices_CompatibleDeviceId",
                table: "GamepadCompatibleDevices",
                column: "CompatibleDeviceId");

            migrationBuilder.CreateIndex(
                name: "IX_GoodsInCarts_GoodsId",
                table: "GoodsInCarts",
                column: "GoodsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gamepads_ConnectionTypes_ConnectionTypeId",
                table: "Gamepads",
                column: "ConnectionTypeId",
                principalTable: "ConnectionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gamepads_Feedbacks_FeedbackId",
                table: "Gamepads",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_Manufacturers_ManufacturerId",
                table: "Goods",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Keyboards_Backlights_BacklightId",
                table: "Keyboards",
                column: "BacklightId",
                principalTable: "Backlights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Keyboards_ConnectionTypes_ConnectionTypeId",
                table: "Keyboards",
                column: "ConnectionTypeId",
                principalTable: "ConnectionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Keyboards_KeyboardSizes_SizeId",
                table: "Keyboards",
                column: "SizeId",
                principalTable: "KeyboardSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Keyboards_KeyboardTypes_TypeId",
                table: "Keyboards",
                column: "TypeId",
                principalTable: "KeyboardTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Keyboards_KeyRollovers_KeyRolloverId",
                table: "Keyboards",
                column: "KeyRolloverId",
                principalTable: "KeyRollovers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Keyboards_Materials_FrameMaterialId",
                table: "Keyboards",
                column: "FrameMaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Keyboards_Materials_KeycapMaterialId",
                table: "Keyboards",
                column: "KeycapMaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_KeyboardSwitches_Manufacturers_ManufacturerId",
                table: "KeyboardSwitches",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mousepads_Backlights_BacklightId",
                table: "Mousepads",
                column: "BacklightId",
                principalTable: "Backlights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mousepads_Materials_BottomMaterialId",
                table: "Mousepads",
                column: "BottomMaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mousepads_Materials_TopMaterialId",
                table: "Mousepads",
                column: "TopMaterialId",
                principalTable: "Materials",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mouses_Backlights_BacklightId",
                table: "Mouses",
                column: "BacklightId",
                principalTable: "Backlights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Mouses_ConnectionTypes_ConnectionTypeId",
                table: "Mouses",
                column: "ConnectionTypeId",
                principalTable: "ConnectionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
