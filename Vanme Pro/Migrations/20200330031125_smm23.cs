using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm23 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemCount",
                table: "PurchaseOrders");

            migrationBuilder.AddColumn<int>(
                name: "ItemsCount",
                table: "PurchaseOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemsCount",
                table: "PurchaseOrders");

            migrationBuilder.AddColumn<int>(
                name: "ItemCount",
                table: "PurchaseOrders",
                type: "int",
                nullable: true);
        }
    }
}
