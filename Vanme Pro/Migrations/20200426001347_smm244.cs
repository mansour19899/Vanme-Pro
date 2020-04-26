using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm244 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Income",
                table: "ProductInventoryWarehouses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OutCome",
                table: "ProductInventoryWarehouses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Income",
                table: "ProductInventoryWarehouses");

            migrationBuilder.DropColumn(
                name: "OutCome",
                table: "ProductInventoryWarehouses");
        }
    }
}
