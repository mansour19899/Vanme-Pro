using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm24 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemsCount",
                table: "PurchaseOrders");

            migrationBuilder.AddColumn<int>(
                name: "ItemsAsnCount",
                table: "PurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemsGrnCount",
                table: "PurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ItemsPoCount",
                table: "PurchaseOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemsAsnCount",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ItemsGrnCount",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "ItemsPoCount",
                table: "PurchaseOrders");

            migrationBuilder.AddColumn<int>(
                name: "ItemsCount",
                table: "PurchaseOrders",
                type: "int",
                nullable: true);
        }
    }
}
