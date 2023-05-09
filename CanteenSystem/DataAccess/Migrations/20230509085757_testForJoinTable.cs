using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EfcDataAccess.Migrations
{
    /// <inheritdoc />
    public partial class testForJoinTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Allergens_Ingredients_IngredientId",
                table: "Allergens");

            migrationBuilder.DropIndex(
                name: "IX_Allergens_IngredientId",
                table: "Allergens");

            migrationBuilder.DropColumn(
                name: "IngredientId",
                table: "Allergens");

            migrationBuilder.CreateTable(
                name: "AllergenIngredient",
                columns: table => new
                {
                    AllergensCode = table.Column<int>(type: "INTEGER", nullable: false),
                    IngredientsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllergenIngredient", x => new { x.AllergensCode, x.IngredientsId });
                    table.ForeignKey(
                        name: "FK_AllergenIngredient_Allergens_AllergensCode",
                        column: x => x.AllergensCode,
                        principalTable: "Allergens",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AllergenIngredient_Ingredients_IngredientsId",
                        column: x => x.IngredientsId,
                        principalTable: "Ingredients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AllergenIngredient_IngredientsId",
                table: "AllergenIngredient",
                column: "IngredientsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AllergenIngredient");

            migrationBuilder.AddColumn<int>(
                name: "IngredientId",
                table: "Allergens",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Allergens_IngredientId",
                table: "Allergens",
                column: "IngredientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Allergens_Ingredients_IngredientId",
                table: "Allergens",
                column: "IngredientId",
                principalTable: "Ingredients",
                principalColumn: "Id");
        }
    }
}
