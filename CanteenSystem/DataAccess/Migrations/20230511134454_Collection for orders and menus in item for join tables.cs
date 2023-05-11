using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfcDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class Collectionforordersandmenusinitemforjointables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Menus_MenuDate",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Orders_OrderId",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_MenuDate",
                table: "Items");

            migrationBuilder.DropIndex(
                name: "IX_Items_OrderId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "MenuDate",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Items");

            migrationBuilder.CreateTable(
                name: "ItemMenu",
                columns: table => new
                {
                    ItemsId = table.Column<int>(type: "INTEGER", nullable: false),
                    MenusDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemMenu", x => new { x.ItemsId, x.MenusDate });
                    table.ForeignKey(
                        name: "FK_ItemMenu_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemMenu_Menus_MenusDate",
                        column: x => x.MenusDate,
                        principalTable: "Menus",
                        principalColumn: "Date",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemOrder",
                columns: table => new
                {
                    ItemsId = table.Column<int>(type: "INTEGER", nullable: false),
                    OrdersId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemOrder", x => new { x.ItemsId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_ItemOrder_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemOrder_Orders_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemMenu_MenusDate",
                table: "ItemMenu",
                column: "MenusDate");

            migrationBuilder.CreateIndex(
                name: "IX_ItemOrder_OrdersId",
                table: "ItemOrder",
                column: "OrdersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemMenu");

            migrationBuilder.DropTable(
                name: "ItemOrder");

            migrationBuilder.AddColumn<DateTime>(
                name: "MenuDate",
                table: "Items",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Items",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_MenuDate",
                table: "Items",
                column: "MenuDate");

            migrationBuilder.CreateIndex(
                name: "IX_Items_OrderId",
                table: "Items",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Menus_MenuDate",
                table: "Items",
                column: "MenuDate",
                principalTable: "Menus",
                principalColumn: "Date");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Orders_OrderId",
                table: "Items",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
