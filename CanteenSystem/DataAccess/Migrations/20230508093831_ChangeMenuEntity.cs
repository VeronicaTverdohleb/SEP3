using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfcDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class ChangeMenuEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Menus_DailyMenuDate",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "DailyMenuDate",
                table: "Items",
                newName: "MenuDate");

            migrationBuilder.RenameIndex(
                name: "IX_Items_DailyMenuDate",
                table: "Items",
                newName: "IX_Items_MenuDate");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Menus_MenuDate",
                table: "Items",
                column: "MenuDate",
                principalTable: "Menus",
                principalColumn: "Date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Menus_MenuDate",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "MenuDate",
                table: "Items",
                newName: "DailyMenuDate");

            migrationBuilder.RenameIndex(
                name: "IX_Items_MenuDate",
                table: "Items",
                newName: "IX_Items_DailyMenuDate");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Menus_DailyMenuDate",
                table: "Items",
                column: "DailyMenuDate",
                principalTable: "Menus",
                principalColumn: "Date");
        }
    }
}
