using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinderClone.Migrations
{
    /// <inheritdoc />
    public partial class IdontRememberTbh : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Folders_NameAndIdDto_ParentFolderId",
                table: "Folders");

            migrationBuilder.DropTable(
                name: "NameAndIdDto");

            migrationBuilder.DropIndex(
                name: "IX_Folders_ParentFolderId",
                table: "Folders");

            migrationBuilder.AddColumn<List<Guid>>(
                name: "FilesIds",
                table: "Folders",
                type: "uuid[]",
                nullable: true);

            migrationBuilder.AddColumn<List<Guid>>(
                name: "SubFoldersIds",
                table: "Folders",
                type: "uuid[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FilesIds",
                table: "Folders");

            migrationBuilder.DropColumn(
                name: "SubFoldersIds",
                table: "Folders");

            migrationBuilder.CreateTable(
                name: "NameAndIdDto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FolderId = table.Column<Guid>(type: "uuid", nullable: true),
                    FolderId1 = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false)
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
                name: "IX_Folders_ParentFolderId",
                table: "Folders",
                column: "ParentFolderId");

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
    }
}
