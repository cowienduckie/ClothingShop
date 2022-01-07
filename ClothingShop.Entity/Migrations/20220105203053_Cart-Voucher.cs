using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingShop.Entity.Migrations
{
    public partial class CartVoucher : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VoucherId",
                table: "Cart",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cart_VoucherId",
                table: "Cart",
                column: "VoucherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_Voucher_VoucherId",
                table: "Cart",
                column: "VoucherId",
                principalTable: "Voucher",
                principalColumn: "VoucherId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_Voucher_VoucherId",
                table: "Cart");

            migrationBuilder.DropIndex(
                name: "IX_Cart_VoucherId",
                table: "Cart");

            migrationBuilder.DropColumn(
                name: "VoucherId",
                table: "Cart");
        }
    }
}