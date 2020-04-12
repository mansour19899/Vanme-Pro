using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm53 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryWarehouses_Products_ProductMaster_fk",
                table: "ProductInventoryWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryWarehouses_Warehouses_Warehouse_fk",
                table: "ProductInventoryWarehouses");

            migrationBuilder.AlterColumn<int>(
                name: "Warehouse_fk",
                table: "ProductInventoryWarehouses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ProductMaster_fk",
                table: "ProductInventoryWarehouses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OnTheWayInventory",
                table: "ProductInventoryWarehouses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Inventory",
                table: "ProductInventoryWarehouses",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryWarehouses_Products_ProductMaster_fk",
                table: "ProductInventoryWarehouses",
                column: "ProductMaster_fk",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryWarehouses_Warehouses_Warehouse_fk",
                table: "ProductInventoryWarehouses",
                column: "Warehouse_fk",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryWarehouses_Products_ProductMaster_fk",
                table: "ProductInventoryWarehouses");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductInventoryWarehouses_Warehouses_Warehouse_fk",
                table: "ProductInventoryWarehouses");

            migrationBuilder.AlterColumn<int>(
                name: "Warehouse_fk",
                table: "ProductInventoryWarehouses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductMaster_fk",
                table: "ProductInventoryWarehouses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OnTheWayInventory",
                table: "ProductInventoryWarehouses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Inventory",
                table: "ProductInventoryWarehouses",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryWarehouses_Products_ProductMaster_fk",
                table: "ProductInventoryWarehouses",
                column: "ProductMaster_fk",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInventoryWarehouses_Warehouses_Warehouse_fk",
                table: "ProductInventoryWarehouses",
                column: "Warehouse_fk",
                principalTable: "Warehouses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
