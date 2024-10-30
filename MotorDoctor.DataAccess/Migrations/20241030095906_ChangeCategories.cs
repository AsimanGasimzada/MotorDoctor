using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorDoctor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeCategories : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Categories",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 11,
                column: "Value",
                value: "facebook.com");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 12,
                column: "Value",
                value: "facebook.com");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 13,
                column: "Value",
                value: "instagramcomg");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 14,
                column: "Value",
                value: "instagramcomg");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 15,
                column: "Value",
                value: "instagramcomg");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 16,
                column: "Value",
                value: "linkedin.com");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 17,
                column: "Value",
                value: "linkedin.com");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 18,
                column: "Value",
                value: "linkedin.com");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 19,
                column: "Value",
                value: "address.com");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 20,
                column: "Value",
                value: "address.com");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 21,
                column: "Value",
                value: "addressp.com");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Categories");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 11,
                column: "Value",
                value: "facebook.png");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 12,
                column: "Value",
                value: "facebook.png");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 13,
                column: "Value",
                value: "instagram.png");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 14,
                column: "Value",
                value: "instagram.png");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 15,
                column: "Value",
                value: "instagram.png");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 16,
                column: "Value",
                value: "linkedin.png");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 17,
                column: "Value",
                value: "linkedin.png");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 18,
                column: "Value",
                value: "linkedin.png");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 19,
                column: "Value",
                value: "address.png");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 20,
                column: "Value",
                value: "address.png");

            migrationBuilder.UpdateData(
                table: "SettingDetails",
                keyColumn: "Id",
                keyValue: 21,
                column: "Value",
                value: "address.png");
        }
    }
}
