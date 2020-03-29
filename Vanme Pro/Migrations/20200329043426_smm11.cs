using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm11 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AsnTotalPerPrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "AsnTotalPrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PoTotalPerPrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PoTotalPrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "TotalQuantity",
                table: "Items");

            migrationBuilder.AddColumn<decimal>(
                name: "AsnTotal",
                table: "PurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "AsnItemsPrice",
                table: "Items",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PoItemsPrice",
                table: "Items",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AsnTotal",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "AsnItemsPrice",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "PoItemsPrice",
                table: "Items");

            migrationBuilder.AddColumn<decimal>(
                name: "AsnTotalPerPrice",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "AsnTotalPrice",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PoTotalPerPrice",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PoTotalPrice",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TotalQuantity",
                table: "Items",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
