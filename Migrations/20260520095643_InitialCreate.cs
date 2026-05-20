using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace blazor_orderbook_simulation.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    order_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    order_side = table.Column<int>(type: "INTEGER", nullable: false),
                    quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    status = table.Column<int>(type: "INTEGER", nullable: false),
                    filled = table.Column<decimal>(type: "TEXT", nullable: false),
                    remaining = table.Column<decimal>(type: "TEXT", nullable: false),
                    OrderType = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false),
                    price = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.order_id);
                });

            migrationBuilder.CreateTable(
                name: "TradeLogs",
                columns: table => new
                {
                    log_id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    taker_id = table.Column<int>(type: "INTEGER", nullable: false),
                    maker_id = table.Column<int>(type: "INTEGER", nullable: false),
                    quantity = table.Column<decimal>(type: "TEXT", nullable: false),
                    execution_price = table.Column<decimal>(type: "TEXT", nullable: false),
                    time = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Makerorder_id = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TradeLogs", x => x.log_id);
                    table.ForeignKey(
                        name: "FK_TradeLogs_Orders_Makerorder_id",
                        column: x => x.Makerorder_id,
                        principalTable: "Orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TradeLogs_Orders_taker_id",
                        column: x => x.taker_id,
                        principalTable: "Orders",
                        principalColumn: "order_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TradeLogs_Makerorder_id",
                table: "TradeLogs",
                column: "Makerorder_id");

            migrationBuilder.CreateIndex(
                name: "IX_TradeLogs_taker_id",
                table: "TradeLogs",
                column: "taker_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TradeLogs");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
