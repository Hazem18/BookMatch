using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class addTicketPurchase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9e392fbd-a3ef-472c-9c3c-91d4ad026581");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b03fc057-03cd-421a-99d1-7fe873299fb6");

            migrationBuilder.CreateTable(
                name: "TicketPurchase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ApplicationName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    SeatNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TicketCategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PaymentStatus = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketPurchase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketPurchase_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketPurchase_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "268b4a4e-0b30-477e-a42d-cae26c7a28f8", null, "Admin", "Admin" },
                    { "8a1016a2-35e6-4186-b164-21e3ea8cd518", null, "User", "User" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_TicketPurchase_TicketId",
                table: "TicketPurchase",
                column: "TicketId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketPurchase_UserId",
                table: "TicketPurchase",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketPurchase");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "268b4a4e-0b30-477e-a42d-cae26c7a28f8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8a1016a2-35e6-4186-b164-21e3ea8cd518");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "9e392fbd-a3ef-472c-9c3c-91d4ad026581", null, "User", "User" },
                    { "b03fc057-03cd-421a-99d1-7fe873299fb6", null, "Admin", "Admin" }
                });
        }
    }
}
