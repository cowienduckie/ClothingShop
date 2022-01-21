using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace ClothingShop.Entity.Migrations
{
    public partial class UpdateOrder2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AcceptTime",
                table: "Order");

            migrationBuilder.AddColumn<DateTime>(
                name: "ApprovalTime",
                table: "Order",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalTime",
                table: "Order");

            migrationBuilder.AddColumn<DateTime>(
                name: "AcceptTime",
                table: "Order",
                type: "datetime2",
                nullable: true);
        }
    }
}