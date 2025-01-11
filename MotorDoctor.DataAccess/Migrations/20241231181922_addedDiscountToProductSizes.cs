using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorDoctor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addedDiscountToProductSizes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Discount",
                table: "ProductSizes",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddCheckConstraint(
                name: "CK_ProductSize_Discount_Range",
                table: "ProductSizes",
                sql: "[Discount] >= 0 AND [Discount] <= 100");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_ProductSize_Discount_Range",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "Discount",
                table: "ProductSizes");
        }
    }
}
