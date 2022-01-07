using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingShop.Entity.Migrations
{
    public partial class RollBackPriceCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Price",
                table: "Product",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Product",
                type: "decimal(18, 0)",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}