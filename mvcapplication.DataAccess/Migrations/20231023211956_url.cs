using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace mvcapplication.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class url : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Product_Id",
                keyValue: 1,
                column: "ImageURL",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Product_Id",
                keyValue: 2,
                column: "ImageURL",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Product_Id",
                keyValue: 3,
                column: "ImageURL",
                value: "");

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Product_Id",
                keyValue: 4,
                column: "ImageURL",
                value: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "Products");
        }
    }
}
