using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class secondmigraiton : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProductModelProductId",
                table: "Categories",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ProductModelProductId",
                table: "Categories",
                column: "ProductModelProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Products_ProductModelProductId",
                table: "Categories",
                column: "ProductModelProductId",
                principalTable: "Products",
                principalColumn: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Products_ProductModelProductId",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ProductModelProductId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ProductModelProductId",
                table: "Categories");
        }
    }
}
