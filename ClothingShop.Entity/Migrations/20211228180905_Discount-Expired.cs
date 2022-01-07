using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingShop.Entity.Migrations
{
    public partial class DiscountExpired : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Discount");

            migrationBuilder.AddColumn<bool>(
                name: "IsExpired",
                table: "Discount",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsExpired",
                table: "Discount");

            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Discount",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}