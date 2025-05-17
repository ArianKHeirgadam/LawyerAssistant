using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerAssistant.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class AddLegalCompanyEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: -1);

            migrationBuilder.AddColumn<int>(
                name: "LegalCompanyId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "LegalCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LegalNationalCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ModDateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RegDateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegalCustomers", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "Gender", "LastName", "ModDateTime", "PasswordHash", "PicPath", "RegDateTime", "Role", "Status", "UserName" },
                values: new object[] { 1, "Admin", true, "User", null, "JfnnlDI7RTiF9RgfG2JNCw==", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "admin" });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_LegalCompanyId",
                table: "Customers",
                column: "LegalCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_LegalCustomers_LegalCompanyId",
                table: "Customers",
                column: "LegalCompanyId",
                principalTable: "LegalCustomers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_LegalCustomers_LegalCompanyId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "LegalCustomers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_LegalCompanyId",
                table: "Customers");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "LegalCompanyId",
                table: "Customers");

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "Gender", "LastName", "ModDateTime", "PasswordHash", "PicPath", "RegDateTime", "Role", "Status", "UserName" },
                values: new object[] { -1, "Admin", true, "User", null, "JfnnlDI7RTiF9RgfG2JNCw==", "", new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 1, "admin" });
        }
    }
}
