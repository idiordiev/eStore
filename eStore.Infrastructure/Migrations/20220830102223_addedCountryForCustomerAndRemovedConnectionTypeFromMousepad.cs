using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore.Infrastructure.Migrations
{
    public partial class addedCountryForCustomerAndRemovedConnectionTypeFromMousepad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Goods_ConnectionTypes_ConnectionTypeId",
                table: "Goods");

            migrationBuilder.DropIndex(
                name: "IX_Goods_ConnectionTypeId",
                table: "Goods");

            migrationBuilder.DropColumn(
                name: "ConnectionTypeId",
                table: "Goods");

            migrationBuilder.AddColumn<string>(
                name: "ShippingCountry",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConnectionTypeId",
                table: "Mouses",
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
                name: "ConnectionTypeId",
                table: "Gamepads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Customers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Mouses_ConnectionTypeId",
                table: "Mouses",
                column: "ConnectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Keyboards_ConnectionTypeId",
                table: "Keyboards",
                column: "ConnectionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Gamepads_ConnectionTypeId",
                table: "Gamepads",
                column: "ConnectionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gamepads_ConnectionTypes_ConnectionTypeId",
                table: "Gamepads",
                column: "ConnectionTypeId",
                principalTable: "ConnectionTypes",
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
                name: "FK_Mouses_ConnectionTypes_ConnectionTypeId",
                table: "Mouses",
                column: "ConnectionTypeId",
                principalTable: "ConnectionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gamepads_ConnectionTypes_ConnectionTypeId",
                table: "Gamepads");

            migrationBuilder.DropForeignKey(
                name: "FK_Keyboards_ConnectionTypes_ConnectionTypeId",
                table: "Keyboards");

            migrationBuilder.DropForeignKey(
                name: "FK_Mouses_ConnectionTypes_ConnectionTypeId",
                table: "Mouses");

            migrationBuilder.DropIndex(
                name: "IX_Mouses_ConnectionTypeId",
                table: "Mouses");

            migrationBuilder.DropIndex(
                name: "IX_Keyboards_ConnectionTypeId",
                table: "Keyboards");

            migrationBuilder.DropIndex(
                name: "IX_Gamepads_ConnectionTypeId",
                table: "Gamepads");

            migrationBuilder.DropColumn(
                name: "ShippingCountry",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ConnectionTypeId",
                table: "Mouses");

            migrationBuilder.DropColumn(
                name: "ConnectionTypeId",
                table: "Keyboards");

            migrationBuilder.DropColumn(
                name: "ConnectionTypeId",
                table: "Gamepads");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Customers");

            migrationBuilder.AddColumn<int>(
                name: "ConnectionTypeId",
                table: "Goods",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Goods_ConnectionTypeId",
                table: "Goods",
                column: "ConnectionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Goods_ConnectionTypes_ConnectionTypeId",
                table: "Goods",
                column: "ConnectionTypeId",
                principalTable: "ConnectionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
