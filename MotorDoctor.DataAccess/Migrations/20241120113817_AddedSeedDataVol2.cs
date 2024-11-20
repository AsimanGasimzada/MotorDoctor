using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MotorDoctor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedDataVol2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key" },
                values: new object[] { 8, "IsSaatlari" });

            migrationBuilder.InsertData(
                table: "SettingDetails",
                columns: new[] { "Id", "LanguageId", "SettingId", "Value" },
                values: new object[,]
                {
                    { 22, 1, 8, "MON-SAT: 09:00-19:00" },
                    { 23, 2, 8, "MON-SAT: 09:00-19:00" },
                    { 24, 3, 8, "MON-SAT: 09:00-19:00" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 8);
        }
    }
}
