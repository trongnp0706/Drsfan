using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Drsfan.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class seedDataForCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Companies",
                columns: new[] { "Id", "Address", "City", "Name", "PhoneNumber", "PostalCode", "State" },
                values: new object[,]
                {
                    { 1, "123 Tech Avenue", "Techville", "Tech Solutions", "555-123-4567", "90001", "CA" },
                    { 2, "456 Global Street", "Innovate City", "Global Innovations", "555-987-6543", "73301", "TX" },
                    { 3, "789 Green Lane", "EcoCity", "EcoWorld Corp", "555-321-6789", "10001", "NY" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
        }
    }
}
