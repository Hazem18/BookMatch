using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Edit_oldMatch_model_Remove_matchId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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
                    { "45449b1a-9359-4390-b88c-d91c7f8b2af2", null, "User", "User" },
                    { "4840c9d4-e601-49c3-83a1-f0980f2a4c94", null, "Admin", "Admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45449b1a-9359-4390-b88c-d91c7f8b2af2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4840c9d4-e601-49c3-83a1-f0980f2a4c94");

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
    }
}
