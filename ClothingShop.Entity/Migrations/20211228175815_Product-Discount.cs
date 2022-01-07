using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingShop.Entity.Migrations
{
    public partial class ProductDiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Product_Discount_DiscountId",
                table: "Product");

            migrationBuilder.DropIndex(
                name: "IX_Product_DiscountId",
                table: "Product");

            migrationBuilder.DropColumn(
                name: "DiscountId",
                table: "Product");

            migrationBuilder.CreateTable(
                name: "ProductDiscount",
                columns: table => new
                {
                    ProductId = table.Column<int>(nullable: false),
                    DiscountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDiscount", x => new { x.ProductId, x.DiscountId });
                    table.ForeignKey(
                        name: "FK_ProductDiscount_Discount_DiscountId",
                        column: x => x.DiscountId,
                        principalTable: "Discount",
                        principalColumn: "DiscountId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductDiscount_Product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Product",
                        principalColumn: "ProductId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductDiscount_DiscountId",
                table: "ProductDiscount",
                column: "DiscountId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductDiscount");

            migrationBuilder.AddColumn<int>(
                name: "DiscountId",
                table: "Product",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Product_DiscountId",
                table: "Product",
                column: "DiscountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Product_Discount_DiscountId",
                table: "Product",
                column: "DiscountId",
                principalTable: "Discount",
                principalColumn: "DiscountId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}