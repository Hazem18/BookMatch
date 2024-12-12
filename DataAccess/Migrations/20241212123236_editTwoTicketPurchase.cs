using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editTwoTicketPurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d80287a7-5b66-4cbf-b654-5411998bdedc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3311758-8b8e-40b0-9505-6b9084165ff8");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateMatch",
                table: "ticketPurchases",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2ae5cba3-6348-4d0d-af5b-7eeb862b2d3f", null, "Admin", "Admin" },
                    { "d043ab8f-2cd2-40d4-bc07-9e9570b7476f", null, "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ae5cba3-6348-4d0d-af5b-7eeb862b2d3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d043ab8f-2cd2-40d4-bc07-9e9570b7476f");

            migrationBuilder.DropColumn(
                name: "DateMatch",
                table: "ticketPurchases");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d80287a7-5b66-4cbf-b654-5411998bdedc", null, "Admin", "Admin" },
                    { "f3311758-8b8e-40b0-9505-6b9084165ff8", null, "User", "User" }
                });
        }
    }
}
