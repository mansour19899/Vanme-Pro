using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm172 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Percent",
                table: "PurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalCharges",
                table: "PurchaseOrders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Percent",
                table: "PurchaseOrders");

            migrationBuilder.DropColumn(
                name: "TotalCharges",
                table: "PurchaseOrders");
        }
    }
}
