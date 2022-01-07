using Microsoft.EntityFrameworkCore.Migrations;

namespace ClothingShop.Entity.Migrations
{
    public partial class UpdateAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber",
                table: "Address",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "Address",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhoneNumber",
                table: "Address");

            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "Address");
        }
    }
}