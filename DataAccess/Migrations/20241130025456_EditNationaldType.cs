using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class EditNationaldType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6dd07b1e-05fc-4581-a53c-4f6496d851a2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "94fc8a7d-b3d2-44f1-ac2b-bc1cbf25146d");

            migrationBuilder.AlterColumn<long>(
                name: "NationalId",
                table: "AspNetUsers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "008de93e-4774-4e1d-a695-d50b2539856c", null, "User", "User" },
                    { "482881a0-5c48-45b6-9842-9eedc88e1dd9", null, "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "008de93e-4774-4e1d-a695-d50b2539856c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "482881a0-5c48-45b6-9842-9eedc88e1dd9");

            migrationBuilder.AlterColumn<int>(
                name: "NationalId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "6dd07b1e-05fc-4581-a53c-4f6496d851a2", null, "Admin", "Admin" },
                    { "94fc8a7d-b3d2-44f1-ac2b-bc1cbf25146d", null, "User", "User" }
                });
        }
    }
}
