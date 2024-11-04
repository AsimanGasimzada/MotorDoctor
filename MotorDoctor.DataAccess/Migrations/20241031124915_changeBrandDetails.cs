using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorDoctor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changeBrandDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BrandDetails_LanguageId",
                table: "BrandDetails");

            migrationBuilder.CreateIndex(
                name: "IX_BrandDetails_LanguageId_BrandId",
                table: "BrandDetails",
                columns: new[] { "LanguageId", "BrandId" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_BrandDetails_LanguageId_BrandId",
                table: "BrandDetails");

            migrationBuilder.CreateIndex(
                name: "IX_BrandDetails_LanguageId",
                table: "BrandDetails",
                column: "LanguageId");
        }
    }
}
