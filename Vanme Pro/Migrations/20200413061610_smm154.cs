using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm154 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fee",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "FeeType",
                table: "PurchaseOrders");

            migrationBuilder.AddColumn<decimal>(
                name: "CustomsDuty",
                table: "PurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Forwarding",
                table: "PurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Handling",
                table: "PurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Insurance",
                table: "PurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "LandTransport",
                table: "PurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Others",
                table: "PurchaseOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomsDuty",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Forwarding",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Handling",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Insurance",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "LandTransport",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Others",
                table: "PurchaseOrders");

            migrationBuilder.AddColumn<decimal>(
                name: "Fee",
                table: "PurchaseOrders",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "FeeType",
                table: "PurchaseOrders",
                type: "decimal(18,2)",
                nullable: true);
        }
    }
}
