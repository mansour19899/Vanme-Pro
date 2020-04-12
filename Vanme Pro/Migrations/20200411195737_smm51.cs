using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm51 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<string>(nullable: true),
                    Gender = table.Column<bool>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    Address3 = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Phone1 = table.Column<string>(nullable: true),
                    Phone2 = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    ImageName = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true),
                    CreatedBy_fk = table.Column<int>(nullable: false),
                    EditedDate = table.Column<DateTime>(nullable: true),
                    LastSaleDate = table.Column<DateTime>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

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
                    ReceiptPrice = table.Column<decimal>(nullable: true),
                    Margin = table.Column<string>(nullable: true),
                    Inventory = table.Column<int>(nullable: false),
                    LastUpdateInventory = table.Column<DateTime>(nullable: false),
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
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    NickName = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true, defaultValue: "0"),
                    Password = table.Column<string>(nullable: true),
                    Lavel = table.Column<int>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false, defaultValue: false),
                    IsVisitor = table.Column<bool>(nullable: false, defaultValue: false),
                    IsCashier = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
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
                name: "Warehouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Tel = table.Column<string>(nullable: true),
                    Note = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductInventoryWarehouses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    ProductMaster_fk = table.Column<int>(nullable: false),
                    Warehouse_fk = table.Column<int>(nullable: false),
                    Inventory = table.Column<int>(nullable: false),
                    OnTheWayInventory = table.Column<int>(nullable: false),
                    Aile = table.Column<string>(nullable: true),
                    Bin = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInventoryWarehouses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInventoryWarehouses_Products_ProductMaster_fk",
                        column: x => x.ProductMaster_fk,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductInventoryWarehouses_Warehouses_Warehouse_fk",
                        column: x => x.Warehouse_fk,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    PoNumber = table.Column<int>(nullable: false),
                    Asnumber = table.Column<int>(nullable: false),
                    Grnumber = table.Column<int>(nullable: false),
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
                    CreateOrder = table.Column<DateTime>(nullable: true),
                    LastEditDate = table.Column<DateTime>(nullable: true),
                    Freight = table.Column<decimal>(nullable: true),
                    DiscountPercent = table.Column<decimal>(nullable: true),
                    DiscountDollers = table.Column<decimal>(nullable: true),
                    FeeType = table.Column<decimal>(nullable: true),
                    Fee = table.Column<decimal>(nullable: true),
                    PoTotal = table.Column<decimal>(nullable: true),
                    AsnTotal = table.Column<decimal>(nullable: true),
                    CreatedPO = table.Column<bool>(nullable: true),
                    CreatedAsn = table.Column<bool>(nullable: true),
                    CreatedGrn = table.Column<bool>(nullable: true),
                    ItemsPoCount = table.Column<int>(nullable: true),
                    ItemsAsnCount = table.Column<int>(nullable: true),
                    ItemsGrnCount = table.Column<int>(nullable: true),
                    ToWarehouse_fk = table.Column<int>(nullable: true),
                    FromWarehouse_fk = table.Column<int>(nullable: true),
                    ApprovePoUser_fk = table.Column<int>(nullable: true),
                    ApproveAsnUser_fk = table.Column<int>(nullable: true),
                    ApproveGrnUser_fk = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Users_ApproveAsnUser_fk",
                        column: x => x.ApproveAsnUser_fk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Users_ApproveGrnUser_fk",
                        column: x => x.ApproveGrnUser_fk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Users_ApprovePoUser_fk",
                        column: x => x.ApprovePoUser_fk,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Warehouses_FromWarehouse_fk",
                        column: x => x.FromWarehouse_fk,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Warehouses_ToWarehouse_fk",
                        column: x => x.ToWarehouse_fk,
                        principalTable: "Warehouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    Cost = table.Column<decimal>(nullable: false),
                    PoItemsPrice = table.Column<decimal>(nullable: false),
                    AsnItemsPrice = table.Column<decimal>(nullable: false),
                    Diffrent = table.Column<int>(nullable: true),
                    Alert = table.Column<bool>(nullable: true, defaultValue: false),
                    Note = table.Column<string>(nullable: true),
                    Checked = table.Column<bool>(nullable: true, defaultValue: false)
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
                name: "IX_ProductInventoryWarehouses_ProductMaster_fk",
                table: "ProductInventoryWarehouses",
                column: "ProductMaster_fk");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInventoryWarehouses_Warehouse_fk",
                table: "ProductInventoryWarehouses",
                column: "Warehouse_fk");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ApproveAsnUser_fk",
                table: "PurchaseOrders",
                column: "ApproveAsnUser_fk");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ApproveGrnUser_fk",
                table: "PurchaseOrders",
                column: "ApproveGrnUser_fk");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ApprovePoUser_fk",
                table: "PurchaseOrders",
                column: "ApprovePoUser_fk");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_FromWarehouse_fk",
                table: "PurchaseOrders",
                column: "FromWarehouse_fk");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_ToWarehouse_fk",
                table: "PurchaseOrders",
                column: "ToWarehouse_fk");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_Vendor_fk",
                table: "PurchaseOrders",
                column: "Vendor_fk");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ProductInventoryWarehouses");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Vendors");
        }
    }
}
