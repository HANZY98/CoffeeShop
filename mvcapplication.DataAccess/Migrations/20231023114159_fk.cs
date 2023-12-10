using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvcapplication.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class fk : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Category_Id",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Product_Id",
                keyValue: 1,
                column: "Category_Id",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Product_Id",
                keyValue: 2,
                column: "Category_Id",
                value: 2);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Product_Id",
                keyValue: 3,
                column: "Category_Id",
                value: 3);

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Product_Id",
                keyValue: 4,
                column: "Category_Id",
                value: 4);

            migrationBuilder.CreateIndex(
                name: "IX_Products_Category_Id",
                table: "Products",
                column: "Category_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products",
                column: "Category_Id",
                principalTable: "Categories",
                principalColumn: "Category_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Categories_Category_Id",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Products_Category_Id",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Category_Id",
                table: "Products");
        }
    }
}
