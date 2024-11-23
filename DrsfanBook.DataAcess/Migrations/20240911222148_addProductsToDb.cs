using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Drsfan.DataAcess.Migrations
{
    /// <inheritdoc />
    public partial class addProductsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ListPrice = table.Column<double>(type: "float", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    Price50 = table.Column<double>(type: "float", nullable: false),
                    Price100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "Description", "ISBN", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 1, "F. Scott Fitzgerald", "A novel written by F. Scott Fitzgerald.", "978-0743273565", 10.99, 9.9900000000000002, 7.9900000000000002, 8.9900000000000002, "The Great Gatsby" },
                    { 2, "Harper Lee", "A novel by Harper Lee published in 1960.", "978-0061120084", 14.99, 13.99, 11.99, 12.99, "To Kill a Mockingbird" },
                    { 3, "George Orwell", "A dystopian novel by George Orwell.", "978-0451524935", 9.9900000000000002, 8.9900000000000002, 6.9900000000000002, 7.9900000000000002, "1984" },
                    { 4, "J.D. Salinger", "A novel by J.D. Salinger.", "978-0316769488", 8.9900000000000002, 7.9900000000000002, 5.9900000000000002, 6.9900000000000002, "The Catcher in the Rye" },
                    { 5, "Jane Austen", "A romantic novel by Jane Austen.", "978-1503290563", 11.99, 10.99, 8.9900000000000002, 9.9900000000000002, "Pride and Prejudice" },
                    { 6, "J.R.R. Tolkien", "A fantasy novel by J.R.R. Tolkien.", "978-0547928227", 12.99, 11.99, 9.9900000000000002, 10.99, "The Hobbit" },
                    { 7, "Herman Melville", "A novel by Herman Melville.", "978-1503280786", 13.99, 12.99, 10.99, 11.99, "Moby Dick" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
