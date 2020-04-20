using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm192 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Provinces",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Hst = table.Column<int>(nullable: true),
                    PSt = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SaleOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Type = table.Column<bool>(nullable: false),
                    OrderedDate = table.Column<DateTime>(nullable: true),
                    SalesOrderNumber = table.Column<int>(nullable: false),
                    Cashier_fk = table.Column<int>(nullable: false),
                    Customer_fk = table.Column<int>(nullable: false),
                    Subtotal = table.Column<decimal>(nullable: false),
                    SoTotal = table.Column<decimal>(nullable: true),
                    TaxArea_fk = table.Column<int>(nullable: false),
                    Tax = table.Column<string>(nullable: true),
                    Doscount = table.Column<byte>(nullable: false),
                    Shipping = table.Column<byte>(nullable: false),
                    FeeName = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleOrders", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Provinces");

            migrationBuilder.DropTable(
                name: "SaleOrders");
        }
    }
}
