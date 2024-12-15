using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Drop_ticketPurchaes_fk_ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticketPurchases_Tickets_TicketId",
                table: "ticketPurchases");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "45449b1a-9359-4390-b88c-d91c7f8b2af2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4840c9d4-e601-49c3-83a1-f0980f2a4c94");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "ticketPurchases",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "MatchId",
                table: "ticketPurchases",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "55341746-c8b4-49df-b8f1-325bf206737e", null, "Admin", "Admin" },
                    { "ba7b96a9-ab12-4955-ae52-1c68cc055a78", null, "User", "User" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ticketPurchases_Tickets_TicketId",
                table: "ticketPurchases",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticketPurchases_Tickets_TicketId",
                table: "ticketPurchases");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "55341746-c8b4-49df-b8f1-325bf206737e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba7b96a9-ab12-4955-ae52-1c68cc055a78");

            migrationBuilder.DropColumn(
                name: "MatchId",
                table: "ticketPurchases");

            migrationBuilder.AlterColumn<int>(
                name: "TicketId",
                table: "ticketPurchases",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "45449b1a-9359-4390-b88c-d91c7f8b2af2", null, "User", "User" },
                    { "4840c9d4-e601-49c3-83a1-f0980f2a4c94", null, "Admin", "Admin" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ticketPurchases_Tickets_TicketId",
                table: "ticketPurchases",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
