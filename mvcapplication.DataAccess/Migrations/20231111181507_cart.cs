using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvcapplication.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class cart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopCart_Products_ProductId",
                table: "ShopCart");

            migrationBuilder.DropIndex(
                name: "IX_ShopCart_ProductId",
                table: "ShopCart");

            migrationBuilder.DropColumn(
                name: "ProductId",
                table: "ShopCart");

            migrationBuilder.CreateIndex(
                name: "IX_ShopCart_Product_Id",
                table: "ShopCart",
                column: "Product_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopCart_Products_Product_Id",
                table: "ShopCart",
                column: "Product_Id",
                principalTable: "Products",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShopCart_Products_Product_Id",
                table: "ShopCart");

            migrationBuilder.DropIndex(
                name: "IX_ShopCart_Product_Id",
                table: "ShopCart");

            migrationBuilder.AddColumn<int>(
                name: "ProductId",
                table: "ShopCart",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShopCart_ProductId",
                table: "ShopCart",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShopCart_Products_ProductId",
                table: "ShopCart",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Product_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
