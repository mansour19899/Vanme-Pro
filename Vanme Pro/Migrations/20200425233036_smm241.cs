using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm241 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SoItem_SaleOrders_SaleOrderIdD",
                table: "SoItem");

            migrationBuilder.DropIndex(
                name: "IX_SoItem_SaleOrderIdD",
                table: "SoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleOrders",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "SaleOrderIdD",
                table: "SoItem");

            migrationBuilder.DropColumn(
                name: "IdD",
                table: "SaleOrders");

            migrationBuilder.AddColumn<int>(
                name: "SaleOrderId",
                table: "SoItem",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "SaleOrders",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleOrders",
                table: "SaleOrders",
                column: "Id");

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

            migrationBuilder.DropPrimaryKey(
                name: "PK_SaleOrders",
                table: "SaleOrders");

            migrationBuilder.DropColumn(
                name: "SaleOrderId",
                table: "SoItem");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "SaleOrders");

            migrationBuilder.AddColumn<int>(
                name: "SaleOrderIdD",
                table: "SoItem",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "IdD",
                table: "SaleOrders",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SaleOrders",
                table: "SaleOrders",
                column: "IdD");

            migrationBuilder.CreateIndex(
                name: "IX_SoItem_SaleOrderIdD",
                table: "SoItem",
                column: "SaleOrderIdD");

            migrationBuilder.AddForeignKey(
                name: "FK_SoItem_SaleOrders_SaleOrderIdD",
                table: "SoItem",
                column: "SaleOrderIdD",
                principalTable: "SaleOrders",
                principalColumn: "IdD",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
