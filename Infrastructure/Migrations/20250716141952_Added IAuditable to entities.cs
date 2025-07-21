using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Web.Migrations
{
    /// <inheritdoc />
    public partial class AddedIAuditabletoentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Folders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Folders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "Folders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Folders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "Folders",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Folders",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Files",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOn",
                table: "Files",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "DeletedById",
                table: "Files",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOn",
                table: "Files",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<Guid>(
                name: "UpdatedById",
                table: "Files",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Files",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Folders_CreatedById",
                table: "Folders",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_DeletedById",
                table: "Folders",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Folders_UpdatedById",
                table: "Folders",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CreatedById",
                table: "Files",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Files_DeletedById",
                table: "Files",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Files_UpdatedById",
                table: "Files",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_CreatedById",
                table: "Files",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_DeletedById",
                table: "Files",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Users_UpdatedById",
                table: "Files",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Users_CreatedById",
                table: "Folders",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Users_DeletedById",
                table: "Folders",
                column: "DeletedById",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Users_UpdatedById",
                table: "Folders",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_CreatedById",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_DeletedById",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Files_Users_UpdatedById",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Users_CreatedById",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Users_DeletedById",
                table: "Folders");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Users_UpdatedById",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_CreatedById",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_DeletedById",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Folders_UpdatedById",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Files_CreatedById",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_DeletedById",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_UpdatedById",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "CreatedOn",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "DeletedOn",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UpdatedById",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Files");
        }
    }
}
