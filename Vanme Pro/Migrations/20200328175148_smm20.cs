using Microsoft.EntityFrameworkCore.Migrations;

namespace Vanme_Pro.Migrations
{
    public partial class smm20 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StyleNumbeer",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "StyleNumber",
                table: "Products",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StyleNumber",
                table: "Products");

            migrationBuilder.AddColumn<string>(
                name: "StyleNumbeer",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
