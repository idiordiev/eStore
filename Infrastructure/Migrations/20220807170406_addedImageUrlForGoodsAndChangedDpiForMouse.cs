using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore.Infrastructure.Migrations
{
    public partial class addedImageUrlForGoodsAndChangedDpiForMouse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SensorDPI",
                table: "Mouses",
                newName: "MinSensorDPI");

            migrationBuilder.AddColumn<int>(
                name: "MaxSensorDPI",
                table: "Mouses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Goods",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxSensorDPI",
                table: "Mouses");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Goods");

            migrationBuilder.RenameColumn(
                name: "MinSensorDPI",
                table: "Mouses",
                newName: "SensorDPI");
        }
    }
}
