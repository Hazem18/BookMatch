using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_booking_date_to_userticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2ae5cba3-6348-4d0d-af5b-7eeb862b2d3f");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d043ab8f-2cd2-40d4-bc07-9e9570b7476f");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDate",
                table: "userTickets",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3e361086-29ec-4f54-92b5-cc38e28fb63a", null, "User", "User" },
                    { "9ceda4dd-9e8d-436c-a783-a4296582575c", null, "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3e361086-29ec-4f54-92b5-cc38e28fb63a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9ceda4dd-9e8d-436c-a783-a4296582575c");

            migrationBuilder.DropColumn(
                name: "BookingDate",
                table: "userTickets");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2ae5cba3-6348-4d0d-af5b-7eeb862b2d3f", null, "Admin", "Admin" },
                    { "d043ab8f-2cd2-40d4-bc07-9e9570b7476f", null, "User", "User" }
                });
        }
    }
}
