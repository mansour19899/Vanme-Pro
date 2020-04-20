using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm193 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Doscount",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "FeeName",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "Shipping",
                table: "SaleOrders");

            migrationBuilder.AlterColumn<int>(
                name: "TaxArea_fk",
                table: "SaleOrders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "SaleOrders",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "SalesOrderNumber",
                table: "SaleOrders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderedDate",
                table: "SaleOrders",
                type: "smalldatetime",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Customer_fk",
                table: "SaleOrders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Cashier_fk",
                table: "SaleOrders",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<DateTime>(
                name: "CancelDate",
                table: "SaleOrders",
                type: "smalldatetime",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Freight",
                table: "SaleOrders",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Handling",
                table: "SaleOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "SaleOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShipMethod_fk",
                table: "SaleOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipToAddressNam1",
                table: "SaleOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipToAddressNam2",
                table: "SaleOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipToAddressName",
                table: "SaleOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipToPostalCode",
                table: "SaleOrders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipToPostalPhone1",
                table: "SaleOrders",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Active",
                table: "Provinces",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "SoItem",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    So_fk = table.Column<int>(nullable: false),
                    ProductMaster_fk = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    TotalPrice = table.Column<decimal>(nullable: false),
                    Discount = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(nullable: false),
                    Quantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoItem", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SoItem_SaleOrders_So_fk",
                        column: x => x.So_fk,
                        principalTable: "SaleOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_Cashier_fk",
                table: "SaleOrders",
                column: "Cashier_fk");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_Customer_fk",
                table: "SaleOrders",
                column: "Customer_fk");

            migrationBuilder.CreateIndex(
                name: "IX_SaleOrders_TaxArea_fk",
                table: "SaleOrders",
                column: "TaxArea_fk");

            migrationBuilder.CreateIndex(
                name: "IX_SoItem_So_fk",
                table: "SoItem",
                column: "So_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrders_Users_Cashier_fk",
                table: "SaleOrders",
                column: "Cashier_fk",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrders_Customers_Customer_fk",
                table: "SaleOrders",
                column: "Customer_fk",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SaleOrders_Provinces_TaxArea_fk",
                table: "SaleOrders",
                column: "TaxArea_fk",
                principalTable: "Provinces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrders_Users_Cashier_fk",
                table: "SaleOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrders_Customers_Customer_fk",
                table: "SaleOrders");

            migrationBuilder.DropForeignKey(
                name: "FK_SaleOrders_Provinces_TaxArea_fk",
                table: "SaleOrders");

            migrationBuilder.DropTable(
                name: "SoItem");

            migrationBuilder.DropIndex(
                name: "IX_SaleOrders_Cashier_fk",
                table: "SaleOrders");

            migrationBuilder.DropIndex(
                name: "IX_SaleOrders_Customer_fk",
                table: "SaleOrders");

            migrationBuilder.DropIndex(
                name: "IX_SaleOrders_TaxArea_fk",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "CancelDate",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "Freight",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "Handling",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "ShipMethod_fk",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "ShipToAddressNam1",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "ShipToAddressNam2",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "ShipToAddressName",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "ShipToPostalCode",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "ShipToPostalPhone1",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "Active",
                table: "Provinces");

            migrationBuilder.AlterColumn<int>(
                name: "TaxArea_fk",
                table: "SaleOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Subtotal",
                table: "SaleOrders",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SalesOrderNumber",
                table: "SaleOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderedDate",
                table: "SaleOrders",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "smalldatetime",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Customer_fk",
                table: "SaleOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Cashier_fk",
                table: "SaleOrders",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Doscount",
                table: "SaleOrders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.AddColumn<int>(
                name: "FeeName",
                table: "SaleOrders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte>(
                name: "Shipping",
                table: "SaleOrders",
                type: "tinyint",
                nullable: false,
                defaultValue: (byte)0);
        }
    }
}
