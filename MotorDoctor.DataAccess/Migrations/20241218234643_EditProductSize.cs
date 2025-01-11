using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorDoctor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditProductSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductSizes_ProductId_Size",
                table: "ProductSizes");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "ProductSizes",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_ProductId_Size",
                table: "ProductSizes",
                columns: new[] { "ProductId", "Size" },
                unique: true,
                filter: "[Size] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ProductSizes_ProductId_Size",
                table: "ProductSizes");

            migrationBuilder.AlterColumn<string>(
                name: "Size",
                table: "ProductSizes",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ProductSizes_ProductId_Size",
                table: "ProductSizes",
                columns: new[] { "ProductId", "Size" },
                unique: true);
        }
    }
}
