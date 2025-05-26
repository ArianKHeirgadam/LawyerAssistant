using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerAssistant.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddingFileTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReActions_Branches_BranchesModelId",
                table: "ReActions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReActions_Complexes_ComplexesModelId",
                table: "ReActions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReActions_Customers_CustomersModelId",
                table: "ReActions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReActions_FilesTypes_FileTypeId",
                table: "ReActions");

            migrationBuilder.DropIndex(
                name: "IX_ReActions_BranchesModelId",
                table: "ReActions");

            migrationBuilder.DropIndex(
                name: "IX_ReActions_ComplexesModelId",
                table: "ReActions");

            migrationBuilder.DropIndex(
                name: "IX_ReActions_CustomersModelId",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "BranchesModelId",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "ComplexesModelId",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "CustomersModelId",
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
                name: "ActionTypeId",
                table: "ReActions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsCompleted",
                table: "ReActions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: true),
                    LegalId = table.Column<int>(type: "int", nullable: true),
                    IsLegal = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    DemandId = table.Column<int>(type: "int", nullable: false),
                    FileTypeId = table.Column<int>(type: "int", nullable: false),
                    ModDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    RegDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
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
                        onDelete: ReferentialAction.Restrict);
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
                name: "IX_ReActions_ActionTypeId",
                table: "ReActions",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_CustomerId",
                table: "Files",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_DemandId",
                table: "Files",
                column: "DemandId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_FileTypeId",
                table: "Files",
                column: "FileTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Files_LegalId",
                table: "Files",
                column: "LegalId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReActions_ActionTypes_ActionTypeId",
                table: "ReActions",
                column: "ActionTypeId",
                principalTable: "ActionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ReActions_Files_FileId",
                table: "ReActions",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReActions_ActionTypes_ActionTypeId",
                table: "ReActions");

            migrationBuilder.DropForeignKey(
                name: "FK_ReActions_Files_FileId",
                table: "ReActions");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_ReActions_ActionTypeId",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "ActionTypeId",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "ReActions");

            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "ReActions",
                newName: "FileTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_ReActions_FileId",
                table: "ReActions",
                newName: "IX_ReActions_FileTypeId");

            migrationBuilder.AddColumn<int>(
                name: "BranchesModelId",
                table: "ReActions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComplexesModelId",
                table: "ReActions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CustomersModelId",
                table: "ReActions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_BranchesModelId",
                table: "ReActions",
                column: "BranchesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_ComplexesModelId",
                table: "ReActions",
                column: "ComplexesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_CustomersModelId",
                table: "ReActions",
                column: "CustomersModelId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReActions_Branches_BranchesModelId",
                table: "ReActions",
                column: "BranchesModelId",
                principalTable: "Branches",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReActions_Complexes_ComplexesModelId",
                table: "ReActions",
                column: "ComplexesModelId",
                principalTable: "Complexes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ReActions_Customers_CustomersModelId",
                table: "ReActions",
                column: "CustomersModelId",
                principalTable: "Customers",
                principalColumn: "Id");

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
