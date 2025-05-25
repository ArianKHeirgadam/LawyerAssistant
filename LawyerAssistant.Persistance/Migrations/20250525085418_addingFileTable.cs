using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerAssistant.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class addingFileTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReActions_FilesTypes_FileTypeId",
                table: "ReActions");

            migrationBuilder.RenameColumn(
                name: "FileTypeId",
                table: "ReActions",
                newName: "FileId");

            migrationBuilder.RenameIndex(
                name: "IX_ReActions_FileTypeId",
                table: "ReActions",
                newName: "IX_ReActions_FileId");

            migrationBuilder.AddColumn<int>(
                name: "FilesTypesModelId",
                table: "ReActions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    DemandId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    LegalId = table.Column<int>(type: "int", nullable: false),
                    IsLegal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    FileTypeId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false),
                    ModDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    RegDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.DemandId);
                    table.ForeignKey(
                        name: "FK_Files_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_Demands_DemandId",
                        column: x => x.DemandId,
                        principalTable: "Demands",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Files_FilesTypes_FileTypeId",
                        column: x => x.FileTypeId,
                        principalTable: "FilesTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Files_LegalCustomers_LegalId",
                        column: x => x.LegalId,
                        principalTable: "LegalCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_FilesTypesModelId",
                table: "ReActions",
                column: "FilesTypesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CustomerId",
                table: "Files",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileTypeId",
                table: "Files",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_LegalId",
                table: "Files",
                column: "LegalId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReActions_FilesTypes_FilesTypesModelId",
                table: "ReActions",
                column: "FilesTypesModelId",
                principalTable: "FilesTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReActions_Files_FileId",
                table: "ReActions",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "DemandId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReActions_FilesTypes_FilesTypesModelId",
                table: "ReActions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReActions_Files_FileId",
                table: "ReActions");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_ReActions_FilesTypesModelId",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "FilesTypesModelId",
                table: "ReActions");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "ReActions",
                newName: "FileTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ReActions_FileId",
                table: "ReActions",
                newName: "IX_ReActions_FileTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReActions_FilesTypes_FileTypeId",
                table: "ReActions",
                column: "FileTypeId",
                principalTable: "FilesTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
