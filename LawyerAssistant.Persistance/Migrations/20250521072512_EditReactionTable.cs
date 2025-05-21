using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerAssistant.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class EditReactionTable : Migration
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
                name: "FK_ReActions_FilesTypes_FilesTypesModelId",
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

            migrationBuilder.DropIndex(
                name: "IX_ReActions_FilesTypesModelId",
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

            migrationBuilder.DropColumn(
                name: "FilesTypesModelId",
                table: "ReActions");

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

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_ActionTypeId",
                table: "ReActions",
                column: "ActionTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReActions_ActionTypes_ActionTypeId",
                table: "ReActions",
                column: "ActionTypeId",
                principalTable: "ActionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReActions_ActionTypes_ActionTypeId",
                table: "ReActions");

            migrationBuilder.DropIndex(
                name: "IX_ReActions_ActionTypeId",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "ActionTypeId",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "IsCompleted",
                table: "ReActions");

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

            migrationBuilder.AddColumn<int>(
                name: "FilesTypesModelId",
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

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_FilesTypesModelId",
                table: "ReActions",
                column: "FilesTypesModelId");

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
                name: "FK_ReActions_FilesTypes_FilesTypesModelId",
                table: "ReActions",
                column: "FilesTypesModelId",
                principalTable: "FilesTypes",
                principalColumn: "Id");
        }
    }
}
