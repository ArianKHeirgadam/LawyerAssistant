using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerAssistant.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class RegDateTimeUnRequired : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demands_FilesTypes_FileId",
                table: "Demands");

            migrationBuilder.DropTable(
                name: "Actions");

            migrationBuilder.DropColumn(
                name: "ModDateTime",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "RegDateTime",
                table: "Provinces");

            migrationBuilder.DropColumn(
                name: "ModDateTime",
                table: "Cities");

            migrationBuilder.DropColumn(
                name: "RegDateTime",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "Demands",
                newName: "FileTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Demands_FileId",
                table: "Demands",
                newName: "IX_Demands_FileTypeId");

            migrationBuilder.CreateTable(
                name: "ActionTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    RememberTime = table.Column<int>(type: "int", nullable: false),
                    ModDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionTypes", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Demands_FilesTypes_FileTypeId",
                table: "Demands",
                column: "FileTypeId",
                principalTable: "FilesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Demands_FilesTypes_FileTypeId",
                table: "Demands");

            migrationBuilder.DropTable(
                name: "ActionTypes");

            migrationBuilder.RenameColumn(
                name: "FileTypeId",
                table: "Demands",
                newName: "FileId");

            migrationBuilder.RenameIndex(
                name: "IX_Demands_FileTypeId",
                table: "Demands",
                newName: "IX_Demands_FileId");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModDateTime",
                table: "Provinces",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegDateTime",
                table: "Provinces",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "ModDateTime",
                table: "Cities",
                type: "datetime",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RegDateTime",
                table: "Cities",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Actions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ModDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    RegDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RememberTime = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actions", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Demands_FilesTypes_FileId",
                table: "Demands",
                column: "FileId",
                principalTable: "FilesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
