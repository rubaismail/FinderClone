using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinderClone.Migrations
{
    /// <inheritdoc />
    public partial class AddCreationDateAndSizeToFilesEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Files",
                type: "date",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "SizeBytes",
                table: "Files",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "SizeBytes",
                table: "Files");
        }
    }
}
