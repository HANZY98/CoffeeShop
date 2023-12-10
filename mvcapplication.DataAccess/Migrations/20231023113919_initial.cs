using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace mvcapplication.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Category_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Category_Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Product_Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SecondName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AboutMe = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salary = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Category_Id", "DisplayOrder", "JobTitle", "Name" },
                values: new object[,]
                {
                    { 1, 1, "GM", "Action" },
                    { 2, 2, "RM", "Adventure" },
                    { 3, 3, "GM", "Comedy" },
                    { 4, 4, "RM", "Drama" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Product_Id", "Author", "Description", "ISBN", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "Bean", "This film is classic", "", 20.0, 200.0, 100.0, "MR Bean 1" },
                    { 2, "Bean", "Another classic", "", 20.0, 200.0, 100.0, "MR Bean 2" },
                    { 3, "Bean", "Yet another classic", "", 20.0, 200.0, 100.0, "MR Bean 3" },
                    { 4, "Unknown", "Not related to Mr. Bean", "", 30.0, 300.0, 150.0, "Another Movie" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "AboutMe", "FirstName", "JobTitle", "Salary", "SecondName" },
                values: new object[,]
                {
                    { 1, "I love coding", "Luke", "Engineer", "100,000", "Hands" },
                    { 2, "I love coding", "Jason", "Engineer", "100,000", "Hands" },
                    { 3, "I love coding", "Dean", "Engineer", "100,000", "Hands" },
                    { 4, "I love coding", "Duke", "Engineer", "100,000", "Hands" },
                    { 5, "I love coding", "Grham", "Engineer", "100,000", "Hands" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
