using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorDoctor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedViewCountinBaseAuditableEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Sliders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "ProductSizes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Comments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "ProductSizes");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "OrderItems");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Categories");
        }
    }
}
