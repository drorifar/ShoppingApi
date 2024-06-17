using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Shopping.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "ID", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "Description", "Name" },
                    { 2, "Description 2", "Name 2" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "ID", "CategoryID", "Description", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 1, "10 boxes x 20 bags", "Chai", 10f },
                    { 2, 1, "24 - 12 oz bottles", "Chang", 20f },
                    { 3, 1, "12 - 355 ml cans", "Guaraná Fantástica", 30f },
                    { 4, 2, "24 - 12 oz bottles", "Sasquatch Ale", 40f },
                    { 5, 2, "24 - 12 oz bottles", "Steeleye Stout", 50f },
                    { 6, 1, "10 boxes x 20 bags", "Chai2", 10f },
                    { 7, 1, "24 - 12 oz bottles", "Chang2", 20f },
                    { 8, 1, "12 - 355 ml cans", "Guaraná Fantástica2", 30f },
                    { 9, 2, "24 - 12 oz bottles", "Sasquatch Ale2", 40f },
                    { 10, 2, "24 - 12 oz bottles", "Steeleye Stout2", 50f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "ID",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "ID",
                keyValue: 2);
        }
    }
}
