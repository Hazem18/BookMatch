using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Edit_oldMatch_model : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "627d3386-cff6-4fa8-aba8-1c8b786b171d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b27db28f-2041-4410-8fec-daed7f406fce");

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "oldMatches",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "66d501f3-f364-43fc-8352-db062ceb9e88", null, "Admin", "Admin" },
                    { "9482cd81-566a-4de9-a0eb-3366189bce0d", null, "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "66d501f3-f364-43fc-8352-db062ceb9e88");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9482cd81-566a-4de9-a0eb-3366189bce0d");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "oldMatches");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "627d3386-cff6-4fa8-aba8-1c8b786b171d", null, "Admin", "Admin" },
                    { "b27db28f-2041-4410-8fec-daed7f406fce", null, "User", "User" }
                });
        }
    }
}
