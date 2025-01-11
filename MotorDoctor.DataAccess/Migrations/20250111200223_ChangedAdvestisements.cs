using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MotorDoctor.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangedAdvestisements : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Advertisements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Advertisements",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Advertisements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "Advertisements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Advertisements",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Advertisements");

            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Advertisements");
        }
    }
}
