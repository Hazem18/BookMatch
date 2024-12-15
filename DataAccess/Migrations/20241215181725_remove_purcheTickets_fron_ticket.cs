using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class remove_purcheTickets_fron_ticket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticketPurchases_Tickets_TicketId",
                table: "ticketPurchases");

            migrationBuilder.DropIndex(
                name: "IX_ticketPurchases_TicketId",
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
                name: "TicketId",
                table: "ticketPurchases");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4fd45c99-23c5-4887-973c-7ee1e535bfbe", null, "Admin", "Admin" },
                    { "c9609ced-67c0-433a-b050-fd70c82830c0", null, "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4fd45c99-23c5-4887-973c-7ee1e535bfbe");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c9609ced-67c0-433a-b050-fd70c82830c0");

            migrationBuilder.AddColumn<int>(
                name: "TicketId",
                table: "ticketPurchases",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "55341746-c8b4-49df-b8f1-325bf206737e", null, "Admin", "Admin" },
                    { "ba7b96a9-ab12-4955-ae52-1c68cc055a78", null, "User", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ticketPurchases_TicketId",
                table: "ticketPurchases",
                column: "TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_ticketPurchases_Tickets_TicketId",
                table: "ticketPurchases",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");
        }
    }
}
