using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ClothingShop.Entity.Migrations
{
    public partial class UpdateCart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AcceptTime",
                table: "Order",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Cart",
                maxLength: 500,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Note",
                table: "Cart");

            migrationBuilder.AlterColumn<DateTime>(
                name: "AcceptTime",
                table: "Order",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}