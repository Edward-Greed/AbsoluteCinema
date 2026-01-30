using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartModelCartId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderModelOrderId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SellerId",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SellerModelSellerId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    Productid = table.Column<int>(type: "int", nullable: false),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    wishlist = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartId);
                });

            migrationBuilder.CreateTable(
                name: "CustomerModels",
                columns: table => new
                {
                    Customerid = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<int>(type: "int", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerModels", x => x.Customerid);
                });

            migrationBuilder.CreateTable(
                name: "SellerModels",
                columns: table => new
                {
                    SellerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phoneNumber = table.Column<int>(type: "int", nullable: false),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    state = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    city = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Zip = table.Column<int>(type: "int", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SellerModels", x => x.SellerId);
                });

            migrationBuilder.CreateTable(
                name: "OrderModels",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    SellerId = table.Column<int>(type: "int", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OrderStatus = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    TrackingNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShippingDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ShippingAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerModelCustomerid = table.Column<int>(type: "int", nullable: true),
                    SellerModelSellerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderModels", x => x.OrderId);
                    table.ForeignKey(
                        name: "FK_OrderModels_CustomerModels_CustomerModelCustomerid",
                        column: x => x.CustomerModelCustomerid,
                        principalTable: "CustomerModels",
                        principalColumn: "Customerid");
                    table.ForeignKey(
                        name: "FK_OrderModels_SellerModels_SellerModelSellerId",
                        column: x => x.SellerModelSellerId,
                        principalTable: "SellerModels",
                        principalColumn: "SellerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CartModelCartId",
                table: "Products",
                column: "CartModelCartId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderModelOrderId",
                table: "Products",
                column: "OrderModelOrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SellerModelSellerId",
                table: "Products",
                column: "SellerModelSellerId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderModels_CustomerModelCustomerid",
                table: "OrderModels",
                column: "CustomerModelCustomerid");

            migrationBuilder.CreateIndex(
                name: "IX_OrderModels_SellerModelSellerId",
                table: "OrderModels",
                column: "SellerModelSellerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Carts_CartModelCartId",
                table: "Products",
                column: "CartModelCartId",
                principalTable: "Carts",
                principalColumn: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_OrderModels_OrderModelOrderId",
                table: "Products",
                column: "OrderModelOrderId",
                principalTable: "OrderModels",
                principalColumn: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_SellerModels_SellerModelSellerId",
                table: "Products",
                column: "SellerModelSellerId",
                principalTable: "SellerModels",
                principalColumn: "SellerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Carts_CartModelCartId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_OrderModels_OrderModelOrderId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_SellerModels_SellerModelSellerId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "OrderModels");

            migrationBuilder.DropTable(
                name: "CustomerModels");

            migrationBuilder.DropTable(
                name: "SellerModels");

            migrationBuilder.DropIndex(
                name: "IX_Products_CartModelCartId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderModelOrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_SellerModelSellerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CartModelCartId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "OrderModelOrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "SellerModelSellerId",
                table: "Products");
        }
    }
}
