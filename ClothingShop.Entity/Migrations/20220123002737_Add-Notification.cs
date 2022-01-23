using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingShop.Entity.Migrations
{
    public partial class AddNotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrederItem_Order_OrderId",
                table: "OrederItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrederItem_ProductEntry_SkuId",
                table: "OrederItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrederItem",
                table: "OrederItem");

            migrationBuilder.RenameTable(
                name: "OrederItem",
                newName: "OrderItem");

            migrationBuilder.RenameIndex(
                name: "IX_OrederItem_SkuId",
                table: "OrderItem",
                newName: "IX_OrderItem_SkuId");

            migrationBuilder.RenameIndex(
                name: "IX_OrederItem_OrderId",
                table: "OrderItem",
                newName: "IX_OrderItem_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem",
                column: "OrderItemId");

            migrationBuilder.CreateTable(
                name: "Notification",
                columns: table => new
                {
                    NotificationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    Message = table.Column<string>(maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notification", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notification_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Notification_UserId",
                table: "Notification",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_ProductEntry_SkuId",
                table: "OrderItem",
                column: "SkuId",
                principalTable: "ProductEntry",
                principalColumn: "SkuId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_ProductEntry_SkuId",
                table: "OrderItem");

            migrationBuilder.DropTable(
                name: "Notification");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                table: "OrderItem");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                newName: "OrederItem");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_SkuId",
                table: "OrederItem",
                newName: "IX_OrederItem_SkuId");

            migrationBuilder.RenameIndex(
                name: "IX_OrderItem_OrderId",
                table: "OrederItem",
                newName: "IX_OrederItem_OrderId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrederItem",
                table: "OrederItem",
                column: "OrderItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrederItem_Order_OrderId",
                table: "OrederItem",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrederItem_ProductEntry_SkuId",
                table: "OrederItem",
                column: "SkuId",
                principalTable: "ProductEntry",
                principalColumn: "SkuId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
