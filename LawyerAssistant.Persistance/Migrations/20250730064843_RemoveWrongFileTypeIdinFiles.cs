using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerAssistant.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RemoveWrongFileTypeIdinFiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_FilesTypes_FileTypeId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_FileTypeId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FileTypeId",
                table: "Files");

            migrationBuilder.AddColumn<int>(
                name: "FilesTypesModelId",
                table: "Files",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Files_FilesTypesModelId",
                table: "Files",
                column: "FilesTypesModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FilesTypes_FilesTypesModelId",
                table: "Files",
                column: "FilesTypesModelId",
                principalTable: "FilesTypes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Files_FilesTypes_FilesTypesModelId",
                table: "Files");

            migrationBuilder.DropIndex(
                name: "IX_Files_FilesTypesModelId",
                table: "Files");

            migrationBuilder.DropColumn(
                name: "FilesTypesModelId",
                table: "Files");

            migrationBuilder.AddColumn<int>(
                name: "FileTypeId",
                table: "Files",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileTypeId",
                table: "Files",
                column: "FileTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Files_FilesTypes_FileTypeId",
                table: "Files",
                column: "FileTypeId",
                principalTable: "FilesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
