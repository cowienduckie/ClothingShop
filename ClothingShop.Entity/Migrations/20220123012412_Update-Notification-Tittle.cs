using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingShop.Entity.Migrations
{
    public partial class UpdateNotificationTittle : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tittle",
                table: "Notification",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tittle",
                table: "Notification");
        }
    }
}
