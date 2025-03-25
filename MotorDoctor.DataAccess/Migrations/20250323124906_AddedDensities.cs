using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorDoctor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddedDensities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DensityId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Densities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    ViewCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Densities", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_DensityId",
                table: "Products",
                column: "DensityId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Densities_DensityId",
                table: "Products",
                column: "DensityId",
                principalTable: "Densities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Densities_DensityId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Densities");

            migrationBuilder.DropIndex(
                name: "IX_Products_DensityId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "DensityId",
                table: "Products");
        }
    }
}
