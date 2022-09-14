using Microsoft.EntityFrameworkCore.Migrations;

namespace eStore.Infrastructure.Migrations.Identity
{
    public partial class addedApplicationUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                "CustomerId",
                "AspNetUsers",
                "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "CustomerId",
                "AspNetUsers");
        }
    }
}