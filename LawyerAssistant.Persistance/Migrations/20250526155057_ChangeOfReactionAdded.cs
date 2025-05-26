using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LawyerAssistant.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOfReactionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReActions_Customers_CustomerId",
                table: "ReActions");

            migrationBuilder.DropIndex(
                name: "IX_ReActions_CustomerId",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "Time",
                table: "ReActions");

            migrationBuilder.AddColumn<int>(
                name: "CustomersModelId",
                table: "ReActions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemember",
                table: "ReActions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSent",
                table: "ReActions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "RememberTime",
                table: "ReActions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "VisitDate",
                table: "ReActions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<TimeSpan>(
                name: "VisitTime",
                table: "ReActions",
                type: "time",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "SMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MessageId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReactionId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    StatusText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    RegDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SMS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SMS_ReActions_ReactionId",
                        column: x => x.ReactionId,
                        principalTable: "ReActions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_CustomersModelId",
                table: "ReActions",
                column: "CustomersModelId");

            migrationBuilder.CreateIndex(
                name: "IX_SMS_ReactionId",
                table: "SMS",
                column: "ReactionId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReActions_Customers_CustomersModelId",
                table: "ReActions",
                column: "CustomersModelId",
                principalTable: "Customers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReActions_Customers_CustomersModelId",
                table: "ReActions");

            migrationBuilder.DropTable(
                name: "SMS");

            migrationBuilder.DropIndex(
                name: "IX_ReActions_CustomersModelId",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "CustomersModelId",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "IsRemember",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "IsSent",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "RememberTime",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "VisitDate",
                table: "ReActions");

            migrationBuilder.DropColumn(
                name: "VisitTime",
                table: "ReActions");

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "ReActions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "Time",
                table: "ReActions",
                type: "datetime",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ReActions_CustomerId",
                table: "ReActions",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReActions_Customers_CustomerId",
                table: "ReActions",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
