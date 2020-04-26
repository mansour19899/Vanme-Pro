using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm246 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Warehouse_fk",
                table: "SaleOrders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_Warehouse_fk",
                table: "SaleOrders",
                column: "Warehouse_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrders_Warehouses_Warehouse_fk",
                table: "SaleOrders",
                column: "Warehouse_fk",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrders_Warehouses_Warehouse_fk",
                table: "SaleOrders");

            migrationBuilder.DropIndex(
                name: "IX_SaleOrders_Warehouse_fk",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "Warehouse_fk",
                table: "SaleOrders");
        }
    }
}
