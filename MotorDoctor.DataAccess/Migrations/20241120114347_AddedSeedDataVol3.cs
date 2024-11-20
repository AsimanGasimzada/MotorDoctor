using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MotorDoctor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedSeedDataVol3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Settings",
                columns: new[] { "Id", "Key" },
                values: new object[] { 9, "Copyright" });

            migrationBuilder.InsertData(
                table: "SettingDetails",
                columns: new[] { "Id", "LanguageId", "SettingId", "Value" },
                values: new object[,]
                {
                    { 25, 1, 9, "© 2023 Qode Interactive , All Rights Reserved" },
                    { 26, 2, 9, "© 2023 Qode Interactive , All Rights Reserved" },
                    { 27, 3, 9, "© 2023 Qode Interactive , All Rights Reserved" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Settings",
                keyColumn: "Id",
                keyValue: 9);
        }
    }
}
