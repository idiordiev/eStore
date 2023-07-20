using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore.Infrastructure.Migrations
{
    public partial class replacedEnumsWithEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Keyboards_Switches_SwitchId",
                table: "Keyboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Switches_Manufacturers_ManufacturerId",
                table: "Switches");

            migrationBuilder.DropTable(
                name: "GamepadCompatibleWiths");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Switches",
                table: "Switches");

            migrationBuilder.DropColumn(
                name: "Backlight",
                table: "Mouses");

            migrationBuilder.DropColumn(
                name: "Backlight",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "ConnectionType",
                table: "Gamepads");

            migrationBuilder.RenameTable(
                name: "Switches",
                newName: "KeyboardSwitches");

            migrationBuilder.RenameColumn(
                name: "ConnectionType",
                table: "Mouses",
                newName: "BacklightId");

            migrationBuilder.RenameColumn(
                name: "TopMaterial",
                table: "Mousepads",
                newName: "TopMaterialId");

            migrationBuilder.RenameColumn(
                name: "BottomMaterial",
                table: "Mousepads",
                newName: "BottomMaterialId");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "Keyboards",
                newName: "TypeId");

            migrationBuilder.RenameColumn(
                name: "Size",
                table: "Keyboards",
                newName: "SizeId");

            migrationBuilder.RenameColumn(
                name: "KeycapMaterial",
                table: "Keyboards",
                newName: "KeycapMaterialId");

            migrationBuilder.RenameColumn(
                name: "KeyRollover",
                table: "Keyboards",
                newName: "KeyRolloverId");

            migrationBuilder.RenameColumn(
                name: "FrameMaterial",
                table: "Keyboards",
                newName: "FrameMaterialId");

            migrationBuilder.RenameColumn(
                name: "ConnectionType",
                table: "Keyboards",
                newName: "BacklightId");

            migrationBuilder.RenameColumn(
                name: "Feedback",
                table: "Gamepads",
                newName: "FeedbackId");

            migrationBuilder.RenameIndex(
                name: "IX_Switches_ManufacturerId",
                table: "KeyboardSwitches",
                newName: "IX_KeyboardSwitches_ManufacturerId");

            migrationBuilder.AlterColumn<string>(
                name: "SensorName",
                table: "Mouses",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BacklightId",
                table: "Mousepads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SwitchId",
                table: "Keyboards",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "IdentityId",
                table: "Customers",
                type: "nvarchar(40)",
                maxLength: 40,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(30)",
                oldMaxLength: 30,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_KeyboardSwitches",
                table: "KeyboardSwitches",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Backlights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Backlights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CompatibleDevicess",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompatibleDevicess", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Feedbacks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "KeyboardSizes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_KeyRollovers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
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
                        name: "FK_GamepadCompatibleDevices_CompatibleDevicess_CompatibleDeviceId",
                        column: x => x.CompatibleDeviceId,
                        principalTable: "CompatibleDevicess",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GamepadCompatibleDevices_Gamepads_GamepadId",
                        column: x => x.GamepadId,
                        principalTable: "Gamepads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DeviceConnectionTypes",
                columns: table => new
                {
                    GoodsId = table.Column<int>(type: "int", nullable: false),
                    ConnectionTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeviceConnectionTypes", x => new { x.GoodsId, x.ConnectionTypeId });
                    table.ForeignKey(
                        name: "FK_DeviceConnectionTypes_ConnectionTypes_ConnectionTypeId",
                        column: x => x.ConnectionTypeId,
                        principalTable: "ConnectionTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DeviceConnectionTypes_Goods_GoodsId",
                        column: x => x.GoodsId,
                        principalTable: "Goods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Mouses_BacklightId",
                table: "Mouses",
                column: "BacklightId");

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
                name: "IX_Keyboards_BacklightId",
                table: "Keyboards",
                column: "BacklightId");

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
                name: "IX_Gamepads_FeedbackId",
                table: "Gamepads",
                column: "FeedbackId");

            migrationBuilder.CreateIndex(
                name: "IX_DeviceConnectionTypes_ConnectionTypeId",
                table: "DeviceConnectionTypes",
                column: "ConnectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_GamepadCompatibleDevices_CompatibleDeviceId",
                table: "GamepadCompatibleDevices",
                column: "CompatibleDeviceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gamepads_Feedbacks_FeedbackId",
                table: "Gamepads",
                column: "FeedbackId",
                principalTable: "Feedbacks",
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
                name: "FK_Keyboards_KeyboardSizes_SizeId",
                table: "Keyboards",
                column: "SizeId",
                principalTable: "KeyboardSizes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Keyboards_KeyboardSwitches_SwitchId",
                table: "Keyboards",
                column: "SwitchId",
                principalTable: "KeyboardSwitches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

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
                onDelete: ReferentialAction.NoAction);

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
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Mouses_Backlights_BacklightId",
                table: "Mouses",
                column: "BacklightId",
                principalTable: "Backlights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gamepads_Feedbacks_FeedbackId",
                table: "Gamepads");

            migrationBuilder.DropForeignKey(
                name: "FK_Keyboards_Backlights_BacklightId",
                table: "Keyboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Keyboards_KeyboardSizes_SizeId",
                table: "Keyboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Keyboards_KeyboardSwitches_SwitchId",
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

            migrationBuilder.DropTable(
                name: "Backlights");

            migrationBuilder.DropTable(
                name: "DeviceConnectionTypes");

            migrationBuilder.DropTable(
                name: "Feedbacks");

            migrationBuilder.DropTable(
                name: "GamepadCompatibleDevices");

            migrationBuilder.DropTable(
                name: "KeyboardSizes");

            migrationBuilder.DropTable(
                name: "KeyboardTypes");

            migrationBuilder.DropTable(
                name: "KeyRollovers");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "ConnectionTypes");

            migrationBuilder.DropTable(
                name: "CompatibleDevicess");

            migrationBuilder.DropIndex(
                name: "IX_Mouses_BacklightId",
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
                name: "IX_Keyboards_BacklightId",
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
                name: "IX_Gamepads_FeedbackId",
                table: "Gamepads");

            migrationBuilder.DropPrimaryKey(
                name: "PK_KeyboardSwitches",
                table: "KeyboardSwitches");

            migrationBuilder.DropColumn(
                name: "BacklightId",
                table: "Mousepads");

            migrationBuilder.RenameTable(
                name: "KeyboardSwitches",
                newName: "Switches");

            migrationBuilder.RenameColumn(
                name: "BacklightId",
                table: "Mouses",
                newName: "ConnectionType");

            migrationBuilder.RenameColumn(
                name: "TopMaterialId",
                table: "Mousepads",
                newName: "TopMaterial");

            migrationBuilder.RenameColumn(
                name: "BottomMaterialId",
                table: "Mousepads",
                newName: "BottomMaterial");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "Keyboards",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "SizeId",
                table: "Keyboards",
                newName: "Size");

            migrationBuilder.RenameColumn(
                name: "KeycapMaterialId",
                table: "Keyboards",
                newName: "KeycapMaterial");

            migrationBuilder.RenameColumn(
                name: "KeyRolloverId",
                table: "Keyboards",
                newName: "KeyRollover");

            migrationBuilder.RenameColumn(
                name: "FrameMaterialId",
                table: "Keyboards",
                newName: "FrameMaterial");

            migrationBuilder.RenameColumn(
                name: "BacklightId",
                table: "Keyboards",
                newName: "ConnectionType");

            migrationBuilder.RenameColumn(
                name: "FeedbackId",
                table: "Gamepads",
                newName: "Feedback");

            migrationBuilder.RenameIndex(
                name: "IX_KeyboardSwitches_ManufacturerId",
                table: "Switches",
                newName: "IX_Switches_ManufacturerId");

            migrationBuilder.AlterColumn<string>(
                name: "SensorName",
                table: "Mouses",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Backlight",
                table: "Mouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SwitchId",
                table: "Keyboards",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Backlight",
                table: "Keyboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ConnectionType",
                table: "Gamepads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "IdentityId",
                table: "Customers",
                type: "nvarchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(40)",
                oldMaxLength: 40,
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Switches",
                table: "Switches",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GamepadCompatibleWiths",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompatibleType = table.Column<int>(type: "int", nullable: false),
                    GamepadId = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamepadCompatibleWiths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GamepadCompatibleWiths_Gamepads_GamepadId",
                        column: x => x.GamepadId,
                        principalTable: "Gamepads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GamepadCompatibleWiths_GamepadId",
                table: "GamepadCompatibleWiths",
                column: "GamepadId");

            migrationBuilder.AddForeignKey(
                name: "FK_Keyboards_Switches_SwitchId",
                table: "Keyboards",
                column: "SwitchId",
                principalTable: "Switches",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Switches_Manufacturers_ManufacturerId",
                table: "Switches",
                column: "ManufacturerId",
                principalTable: "Manufacturers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
