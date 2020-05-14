using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class changeInvenrtory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "SoItem");

            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OnTheWayInventory",
                table: "Products");

            migrationBuilder.AddColumn<bool>(
                name: "IsReserved",
                table: "SoItem",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Outcome",
                table: "Products",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Income",
                table: "Products",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "GoodsReserved",
                table: "Products",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "StockOnHand",
                table: "Products",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsReserved",
                table: "SoItem");

            migrationBuilder.DropColumn(
                name: "GoodsReserved",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "StockOnHand",
                table: "Products");

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "SoItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "Outcome",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Income",
                table: "Products",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Inventory",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OnTheWayInventory",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
