using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class add_view_bool_to_supportTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "33c3c1a6-ed50-46dd-8fb0-221a56f161cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d935eaa8-bc72-4c08-91c8-b883208ee48e");

            migrationBuilder.AddColumn<bool>(
                name: "IsViewed",
                table: "SupportTickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "566d015a-e05f-45b6-8517-aaaa00bd2487", null, "User", "User" },
                    { "9c777ddf-7a0e-4ed1-bb80-2730a59d0a7b", null, "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "566d015a-e05f-45b6-8517-aaaa00bd2487");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9c777ddf-7a0e-4ed1-bb80-2730a59d0a7b");

            migrationBuilder.DropColumn(
                name: "IsViewed",
                table: "SupportTickets");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "33c3c1a6-ed50-46dd-8fb0-221a56f161cc", null, "Admin", "Admin" },
                    { "d935eaa8-bc72-4c08-91c8-b883208ee48e", null, "User", "User" }
                });
        }
    }
}
