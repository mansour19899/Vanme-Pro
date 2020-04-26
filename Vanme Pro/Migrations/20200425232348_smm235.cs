using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm235 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoItem_SaleOrders_So_fk",
                table: "SoItem");

            migrationBuilder.DropIndex(
                name: "IX_SoItem_So_fk",
                table: "SoItem");

            migrationBuilder.AddColumn<int>(
                name: "SaleOrderId",
                table: "SoItem",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SoItem_SaleOrderId",
                table: "SoItem",
                column: "SaleOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_SoItem_SaleOrders_SaleOrderId",
                table: "SoItem",
                column: "SaleOrderId",
                principalTable: "SaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoItem_SaleOrders_SaleOrderId",
                table: "SoItem");

            migrationBuilder.DropIndex(
                name: "IX_SoItem_SaleOrderId",
                table: "SoItem");

            migrationBuilder.DropColumn(
                name: "SaleOrderId",
                table: "SoItem");

            migrationBuilder.CreateIndex(
                name: "IX_SoItem_So_fk",
                table: "SoItem",
                column: "So_fk");

            migrationBuilder.AddForeignKey(
                name: "FK_SoItem_SaleOrders_So_fk",
                table: "SoItem",
                column: "So_fk",
                principalTable: "SaleOrders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
