using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm195 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_SoItem_ProductMaster_fk",
                table: "SoItem",
                column: "ProductMaster_fk");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_CreatedBy_fk",
                table: "Customers",
                column: "CreatedBy_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_CreatedBy_fk",
                table: "Customers",
                column: "CreatedBy_fk",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SoItem_Products_ProductMaster_fk",
                table: "SoItem",
                column: "ProductMaster_fk",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_CreatedBy_fk",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_SoItem_Products_ProductMaster_fk",
                table: "SoItem");

            migrationBuilder.DropIndex(
                name: "IX_SoItem_ProductMaster_fk",
                table: "SoItem");

            migrationBuilder.DropIndex(
                name: "IX_Customers_CreatedBy_fk",
                table: "Customers");
        }
    }
}
