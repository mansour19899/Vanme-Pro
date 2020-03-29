using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm10 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    VendorCode = table.Column<int>(nullable: false),
                    StyleNumber = table.Column<string>(nullable: true),
                    SKU = table.Column<string>(nullable: true),
                    UPC = table.Column<string>(nullable: true),
                    Size = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true),
                    MadeIn = table.Column<string>(nullable: true),
                    Cost = table.Column<decimal>(nullable: true),
                    Price = table.Column<decimal>(nullable: true),
                    Margin = table.Column<string>(nullable: true),
                    Inventory = table.Column<int>(nullable: false),
                    Income = table.Column<int>(nullable: false),
                    Outcome = table.Column<int>(nullable: false),
                    OnTheWayInventory = table.Column<int>(nullable: false),
                    Image = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vendors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    Address3 = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Phone1 = table.Column<string>(nullable: true),
                    Phone2 = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Acountsharp = table.Column<string>(nullable: true),
                    PaymentTerms = table.Column<string>(nullable: true),
                    TradeDiscountPercent = table.Column<string>(nullable: true),
                    Currency = table.Column<string>(nullable: true),
                    LeadTime = table.Column<string>(nullable: true),
                    Info1 = table.Column<string>(nullable: true),
                    Info2 = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ItemCount = table.Column<int>(nullable: true),
                    PoNumber = table.Column<string>(nullable: true),
                    Vendor_fk = table.Column<int>(nullable: true),
                    PoType = table.Column<string>(nullable: true),
                    ShipToStore = table.Column<string>(nullable: true),
                    Associate = table.Column<string>(nullable: true),
                    PoTerms = table.Column<string>(nullable: true),
                    Account = table.Column<string>(nullable: true),
                    FormSO = table.Column<string>(nullable: true),
                    OrderDate = table.Column<DateTime>(nullable: true),
                    AsnDate = table.Column<DateTime>(nullable: true),
                    GrnDate = table.Column<DateTime>(nullable: true),
                    ShipDate = table.Column<DateTime>(nullable: true),
                    CancelDate = table.Column<DateTime>(nullable: true),
                    LastEditDate = table.Column<DateTime>(nullable: true),
                    Freight = table.Column<decimal>(nullable: true),
                    DiscountPercent = table.Column<decimal>(nullable: true),
                    DiscountDollers = table.Column<decimal>(nullable: true),
                    FeeType = table.Column<decimal>(nullable: true),
                    Fee = table.Column<decimal>(nullable: true),
                    PoTotal = table.Column<decimal>(nullable: true),
                    CreatedPO = table.Column<bool>(nullable: true),
                    CreatedAsn = table.Column<bool>(nullable: true),
                    CreatedGrn = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Vendors_Vendor_fk",
                        column: x => x.Vendor_fk,
                        principalTable: "Vendors",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Po_fk = table.Column<int>(nullable: false),
                    ProductMaster_fk = table.Column<int>(nullable: false),
                    PoQuantity = table.Column<int>(nullable: false, defaultValue: 0),
                    AsnQuantity = table.Column<int>(nullable: false, defaultValue: 0),
                    GrnQuantity = table.Column<int>(nullable: false, defaultValue: 0),
                    PoPrice = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    AsnPrice = table.Column<decimal>(nullable: false),
                    TotalQuantity = table.Column<int>(nullable: false),
                    PoTotalPerPrice = table.Column<decimal>(nullable: false, defaultValue: 0m),
                    AsnTotalPerPrice = table.Column<decimal>(nullable: false),
                    PoTotalPrice = table.Column<decimal>(nullable: false),
                    AsnTotalPrice = table.Column<decimal>(nullable: false),
                    Note = table.Column<string>(type: "text", nullable: true, defaultValue: "Note :  ")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_PurchaseOrders_Po_fk",
                        column: x => x.Po_fk,
                        principalTable: "PurchaseOrders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Items_Products_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_Po_fk",
                table: "Items",
                column: "Po_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ProductMaster_fk",
                table: "Items",
                column: "ProductMaster_fk");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_Vendor_fk",
                table: "PurchaseOrders",
                column: "Vendor_fk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
