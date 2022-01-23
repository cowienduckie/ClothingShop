using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ClothingShop.Entity.Migrations
{
    public partial class UpdateOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptTime",
                table: "Order",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Order",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptTime",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Order");
        }
    }
}