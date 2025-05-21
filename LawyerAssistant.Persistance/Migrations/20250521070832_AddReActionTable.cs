using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerAssistant.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddReActionTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReActions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Time = table.Column<DateTime>(type: "datetime", nullable: true),
                    TimeIsImportant = table.Column<bool>(type: "bit", nullable: false),
                    GoingToBranch = table.Column<bool>(type: "bit", nullable: false),
                    BranchId = table.Column<int>(type: "int", nullable: true),
                    ComplexeId = table.Column<int>(type: "int", nullable: true),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    FileTypeId = table.Column<int>(type: "int", nullable: false),
                    BranchesModelId = table.Column<int>(type: "int", nullable: true),
                    ComplexesModelId = table.Column<int>(type: "int", nullable: true),
                    CustomersModelId = table.Column<int>(type: "int", nullable: true),
                    FilesTypesModelId = table.Column<int>(type: "int", nullable: true),
                    ModDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    RegDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReActions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReActions_Branches_BranchId",
                        column: x => x.BranchId,
                        principalTable: "Branches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReActions_Branches_BranchesModelId",
                        column: x => x.BranchesModelId,
                        principalTable: "Branches",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReActions_Complexes_ComplexeId",
                        column: x => x.ComplexeId,
                        principalTable: "Complexes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReActions_Complexes_ComplexesModelId",
                        column: x => x.ComplexesModelId,
                        principalTable: "Complexes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReActions_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReActions_Customers_CustomersModelId",
                        column: x => x.CustomersModelId,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReActions_FilesTypes_FileTypeId",
                        column: x => x.FileTypeId,
                        principalTable: "FilesTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReActions_FilesTypes_FilesTypesModelId",
                        column: x => x.FilesTypesModelId,
                        principalTable: "FilesTypes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_BranchesModelId",
                table: "ReActions",
                column: "BranchesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_BranchId",
                table: "ReActions",
                column: "BranchId");

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_ComplexeId",
                table: "ReActions",
                column: "ComplexeId");

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_ComplexesModelId",
                table: "ReActions",
                column: "ComplexesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_CustomerId",
                table: "ReActions",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_CustomersModelId",
                table: "ReActions",
                column: "CustomersModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_FilesTypesModelId",
                table: "ReActions",
                column: "FilesTypesModelId");

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_FileTypeId",
                table: "ReActions",
                column: "FileTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReActions");
        }
    }
}
