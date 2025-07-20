using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinderClone.Migrations
{
    /// <inheritdoc />
    public partial class ParentFolderId_key : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_Folders_FolderId",
                table: "Files");

            migrationBuilder.DropForeignKey(
                name: "FK_Folders_Folders_ParentFolderId",
                table: "Folders");

            migrationBuilder.DropIndex(
                name: "IX_Files_FolderId",
                table: "Files");

            migrationBuilder.RenameColumn(
                name: "FolderId",
                table: "Files",
                newName: "ParentFolderId");

            migrationBuilder.CreateTable(
                name: "NameAndIdDto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    FolderId = table.Column<Guid>(type: "uuid", nullable: true),
                    FolderId1 = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NameAndIdDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NameAndIdDto_Folders_FolderId",
                        column: x => x.FolderId,
                        principalTable: "Folders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_NameAndIdDto_Folders_FolderId1",
                        column: x => x.FolderId1,
                        principalTable: "Folders",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_NameAndIdDto_FolderId",
                table: "NameAndIdDto",
                column: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_NameAndIdDto_FolderId1",
                table: "NameAndIdDto",
                column: "FolderId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_NameAndIdDto_ParentFolderId",
                table: "Folders",
                column: "ParentFolderId",
                principalTable: "NameAndIdDto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_NameAndIdDto_ParentFolderId",
                table: "Folders");

            migrationBuilder.DropTable(
                name: "NameAndIdDto");

            migrationBuilder.RenameColumn(
                name: "ParentFolderId",
                table: "Files",
                newName: "FolderId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FolderId",
                table: "Files",
                column: "FolderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_Folders_FolderId",
                table: "Files",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Folders_Folders_ParentFolderId",
                table: "Folders",
                column: "ParentFolderId",
                principalTable: "Folders",
                principalColumn: "Id");
        }
    }
}
