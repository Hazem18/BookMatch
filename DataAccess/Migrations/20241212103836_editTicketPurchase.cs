using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class editTicketPurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketPurchase_AspNetUsers_UserId",
                table: "TicketPurchase");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketPurchase_Tickets_TicketId",
                table: "TicketPurchase");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TicketPurchase",
                table: "TicketPurchase");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "268b4a4e-0b30-477e-a42d-cae26c7a28f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a1016a2-35e6-4186-b164-21e3ea8cd518");

            migrationBuilder.RenameTable(
                name: "TicketPurchase",
                newName: "ticketPurchases");

            migrationBuilder.RenameIndex(
                name: "IX_TicketPurchase_UserId",
                table: "ticketPurchases",
                newName: "IX_ticketPurchases_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TicketPurchase_TicketId",
                table: "ticketPurchases",
                newName: "IX_ticketPurchases_TicketId");

            migrationBuilder.AddColumn<string>(
                name: "StadiumName",
                table: "ticketPurchases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeamAName",
                table: "ticketPurchases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TeamBName",
                table: "ticketPurchases",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ticketPurchases",
                table: "ticketPurchases",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "d80287a7-5b66-4cbf-b654-5411998bdedc", null, "Admin", "Admin" },
                    { "f3311758-8b8e-40b0-9505-6b9084165ff8", null, "User", "User" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ticketPurchases_AspNetUsers_UserId",
                table: "ticketPurchases",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ticketPurchases_Tickets_TicketId",
                table: "ticketPurchases",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ticketPurchases_AspNetUsers_UserId",
                table: "ticketPurchases");

            migrationBuilder.DropForeignKey(
                name: "FK_ticketPurchases_Tickets_TicketId",
                table: "ticketPurchases");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ticketPurchases",
                table: "ticketPurchases");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d80287a7-5b66-4cbf-b654-5411998bdedc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f3311758-8b8e-40b0-9505-6b9084165ff8");

            migrationBuilder.DropColumn(
                name: "StadiumName",
                table: "ticketPurchases");

            migrationBuilder.DropColumn(
                name: "TeamAName",
                table: "ticketPurchases");

            migrationBuilder.DropColumn(
                name: "TeamBName",
                table: "ticketPurchases");

            migrationBuilder.RenameTable(
                name: "ticketPurchases",
                newName: "TicketPurchase");

            migrationBuilder.RenameIndex(
                name: "IX_ticketPurchases_UserId",
                table: "TicketPurchase",
                newName: "IX_TicketPurchase_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ticketPurchases_TicketId",
                table: "TicketPurchase",
                newName: "IX_TicketPurchase_TicketId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TicketPurchase",
                table: "TicketPurchase",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "268b4a4e-0b30-477e-a42d-cae26c7a28f8", null, "Admin", "Admin" },
                    { "8a1016a2-35e6-4186-b164-21e3ea8cd518", null, "User", "User" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPurchase_AspNetUsers_UserId",
                table: "TicketPurchase",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketPurchase_Tickets_TicketId",
                table: "TicketPurchase",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
