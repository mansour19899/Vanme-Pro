using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Vendors");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Vendors",
                newName: "ID");

            migrationBuilder.AddColumn<string>(
                name: "Acountsharp",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address1",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address2",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Address3",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Country",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Currency",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Info1",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Info2",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LeadTime",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PaymentTerms",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone1",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Phone2",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PostalCode",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TradeDiscountPercent",
                table: "Vendors",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Items",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Items",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Items",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "PoQuantity",
                table: "Items",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "GrnQuantity",
                table: "Items",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AsnQuantity",
                table: "Items",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "Note",
                table: "Items",
                type: "text",
                nullable: true,
                defaultValue: "Note :  ");

            migrationBuilder.CreateIndex(
                name: "IX_Vendors_ID",
                table: "Vendors",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Po_fk",
                table: "Items",
                column: "Po_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Items_ProductMaster_fk",
                table: "Items",
                column: "ProductMaster_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_PurchaseOrders_Po_fk",
                table: "Items",
                column: "Po_fk",
                principalTable: "PurchaseOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Products_ProductMaster_fk",
                table: "Items",
                column: "ProductMaster_fk",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_PurchaseOrders_Po_fk",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Products_ProductMaster_fk",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Vendors_ID",
                table: "Vendors");

            migrationBuilder.DropIndex(
                name: "IX_Items_Po_fk",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_ProductMaster_fk",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "Acountsharp",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Address1",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Address2",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Address3",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Country",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Currency",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Info1",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Info2",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "LeadTime",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "PaymentTerms",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Phone1",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Phone2",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "PostalCode",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "TradeDiscountPercent",
                table: "Vendors");

            migrationBuilder.DropColumn(
                name: "Note",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "ID",
                table: "Vendors",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Vendors",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "TotalPrice",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Items",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldDefaultValue: 0m);

            migrationBuilder.AlterColumn<int>(
                name: "PoQuantity",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "GrnQuantity",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "AsnQuantity",
                table: "Items",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldDefaultValue: 0);
        }
    }
}
