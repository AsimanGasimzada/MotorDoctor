using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MotorDoctor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedProductsAndAddedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SalesCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Statuses",
                column: "Id",
                value: 4);

            migrationBuilder.InsertData(
                table: "StatusDetails",
                columns: new[] { "Id", "LanguageId", "Name", "StatusId" },
                values: new object[,]
                {
                    { 10, 1, "Ləğv edildi", 4 },
                    { 11, 2, "Cancelled", 4 },
                    { 12, 3, "Отменено", 4 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StatusDetails",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "StatusDetails",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "StatusDetails",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Statuses",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DropColumn(
                name: "SalesCount",
                table: "Products");
        }
    }
}
