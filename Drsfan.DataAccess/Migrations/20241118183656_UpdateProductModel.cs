using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Drsfan.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class UpdateProductModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Companies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Price100",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Products",
                newName: "WarrantyPeriod");

            migrationBuilder.RenameColumn(
                name: "Price50",
                table: "Products",
                newName: "DiscountPrice");

            migrationBuilder.RenameColumn(
                name: "ISBN",
                table: "Products",
                newName: "PowerConsumption");

            migrationBuilder.RenameColumn(
                name: "Author",
                table: "Products",
                newName: "Name");

            migrationBuilder.AddColumn<string>(
                name: "Brand",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Features",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ModelNumber",
                table: "Products",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Washing Machines");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "Refrigerators");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "Microwaves");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DisplayOrder", "Name" },
                values: new object[] { 4, 4, "Fans" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Brand", "Description", "DiscountPrice", "Features", "ImageUrl", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[] { "LG", "Front load washing machine, inverter technology, energy-efficient.", 299.99000000000001, "Automatic wash mode, quick wash, energy-saving", "/images/products/lggiat.jpg", 349.99000000000001, "LG1234", "LG 8kg Washing Machine", "500W", "24 months" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Brand", "Description", "DiscountPrice", "Features", "ImageUrl", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[] { "Samsung", "Double-door refrigerator, 250L capacity, energy-saving technology.", 459.99000000000001, "Quick cooling, energy-efficient", "/images/products/samsungtu.jpg", 499.99000000000001, "Samsung250L", "Samsung 250L Refrigerator", "150W", "12 months" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Brand", "Description", "DiscountPrice", "Features", "ImageUrl", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[] { "Sharp", "Sharp 23L microwave with grilling function, time-saving cooking.", 99.989999999999995, "Grilling and microwave functions, easy to use", "/images/products/sharplosong.jpg", 119.98999999999999, "Sharp23L", "Sharp 23L Microwave", "800W", "12 months" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Brand", "CategoryId", "Description", "DiscountPrice", "Features", "ImageUrl", "ListPrice", "ModelNumber", "Name", "PowerConsumption", "WarrantyPeriod" },
                values: new object[] { "Panasonic", 4, "Panasonic stand fan with 3 wind modes, durable motor.", 29.989999999999998, "Strong wind modes, energy-saving", "/images/products/panasonicquat.jpg", 39.990000000000002, "Panasonic16", "Panasonic 16-inch Fan", "50W", "12 months" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "Brand",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "Features",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ModelNumber",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "WarrantyPeriod",
                table: "Products",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "PowerConsumption",
                table: "Products",
                newName: "ISBN");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "Author");

            migrationBuilder.RenameColumn(
                name: "DiscountPrice",
                table: "Products",
                newName: "Price50");

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Price100",
                table: "Products",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "Action");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "SciFi");

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "History");

            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "City", "Name", "PhoneNumber", "PostalCode", "State" },
                values: new object[,]
                {
                    { 1, "123 Tech Avenue", "Techville", "Tech Solutions", "555-123-4567", "90001", "CA" },
                    { 2, "456 Global Street", "Innovate City", "Global Innovations", "555-987-6543", "73301", "TX" },
                    { 3, "789 Green Lane", "EcoCity", "EcoWorld Corp", "555-321-6789", "10001", "NY" }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Author", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[] { "F. Scott Fitzgerald", "A novel written by F. Scott Fitzgerald.", "978-0743273565", "", 10.99, 9.9900000000000002, 7.9900000000000002, 8.9900000000000002, "The Great Gatsby" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Author", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[] { "Harper Lee", "A novel by Harper Lee published in 1960.", "978-0061120084", "", 14.99, 13.99, 11.99, 12.99, "To Kill a Mockingbird" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Author", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[] { "George Orwell", "A dystopian novel by George Orwell.", "978-0451524935", "", 9.9900000000000002, 8.9900000000000002, 6.9900000000000002, 7.9900000000000002, "1984" });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Author", "CategoryId", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[] { "J.D. Salinger", 1, "A novel by J.D. Salinger.", "978-0316769488", "", 8.9900000000000002, 7.9900000000000002, 5.9900000000000002, 6.9900000000000002, "The Catcher in the Rye" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Author", "CategoryId", "Description", "ISBN", "ImageUrl", "ListPrice", "Price", "Price100", "Price50", "Title" },
                values: new object[,]
                {
                    { 5, "Jane Austen", 1, "A romantic novel by Jane Austen.", "978-1503290563", "", 11.99, 10.99, 8.9900000000000002, 9.9900000000000002, "Pride and Prejudice" },
                    { 6, "J.R.R. Tolkien", 2, "A fantasy novel by J.R.R. Tolkien.", "978-0547928227", "", 12.99, 11.99, 9.9900000000000002, 10.99, "The Hobbit" },
                    { 7, "Herman Melville", 3, "A novel by Herman Melville.", "978-1503280786", "", 13.99, 12.99, 10.99, 11.99, "Moby Dick" }
                });
        }
    }
}
