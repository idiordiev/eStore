using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore.Infrastructure.Migrations
{
    public partial class removedRedundantFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamepadCompatibleDevices_CompatibleDevicess_CompatibleDeviceId",
                table: "GamepadCompatibleDevices");

            migrationBuilder.DropTable(
                name: "DeviceConnectionTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompatibleDevicess",
                table: "CompatibleDevicess");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "Mousepads");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Customers");

            migrationBuilder.RenameTable(
                name: "CompatibleDevicess",
                newName: "CompatibleDevices");

            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Goods",
                newName: "ThumbnailImageUrl");

            migrationBuilder.AddColumn<string>(
                name: "BigImageUrl",
                table: "Goods",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConnectionTypeId",
                table: "Goods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompatibleDevices",
                table: "CompatibleDevices",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Goods_ConnectionTypeId",
                table: "Goods",
                column: "ConnectionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamepadCompatibleDevices_CompatibleDevices_CompatibleDeviceId",
                table: "GamepadCompatibleDevices",
                column: "CompatibleDeviceId",
                principalTable: "CompatibleDevices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_ConnectionTypes_ConnectionTypeId",
                table: "Goods",
                column: "ConnectionTypeId",
                principalTable: "ConnectionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GamepadCompatibleDevices_CompatibleDevices_CompatibleDeviceId",
                table: "GamepadCompatibleDevices");

            migrationBuilder.DropForeignKey(
                name: "FK_Goods_ConnectionTypes_ConnectionTypeId",
                table: "Goods");

            migrationBuilder.DropIndex(
                name: "IX_Goods_ConnectionTypeId",
                table: "Goods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompatibleDevices",
                table: "CompatibleDevices");

            migrationBuilder.DropColumn(
                name: "BigImageUrl",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "ConnectionTypeId",
                table: "Goods");

            migrationBuilder.RenameTable(
                name: "CompatibleDevices",
                newName: "CompatibleDevicess");

            migrationBuilder.RenameColumn(
                name: "ThumbnailImageUrl",
                table: "Goods",
                newName: "ImageUrl");

            migrationBuilder.AddColumn<float>(
                name: "Weight",
                table: "Mousepads",
                type: "real",
                nullable: false,
                defaultValue: 0f);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Customers",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompatibleDevicess",
                table: "CompatibleDevicess",
                column: "Id");

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
                name: "IX_DeviceConnectionTypes_ConnectionTypeId",
                table: "DeviceConnectionTypes",
                column: "ConnectionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_GamepadCompatibleDevices_CompatibleDevicess_CompatibleDeviceId",
                table: "GamepadCompatibleDevices",
                column: "CompatibleDeviceId",
                principalTable: "CompatibleDevicess",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
