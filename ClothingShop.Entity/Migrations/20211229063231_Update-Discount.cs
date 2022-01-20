using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingShop.Entity.Migrations
{
    public partial class UpdateDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Percentage",
                table: "Discount",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(4, 2)");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Discount",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Discount");

            migrationBuilder.AlterColumn<decimal>(
                name: "Percentage",
                table: "Discount",
                type: "decimal(4, 2)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}