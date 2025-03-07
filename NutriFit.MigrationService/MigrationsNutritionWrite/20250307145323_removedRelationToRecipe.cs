using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriFit.MigrationService.MigrationsNutritionWrite
{
    /// <inheritdoc />
    public partial class removedRelationToRecipe : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MealSlotRecipes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MealSlotRecipes",
                columns: table => new
                {
                    MealSlotId = table.Column<Guid>(type: "uuid", nullable: false),
                    RecipeId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealSlotRecipes", x => new { x.MealSlotId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_MealSlotRecipes_MealSlots_MealSlotId",
                        column: x => x.MealSlotId,
                        principalTable: "MealSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealSlotRecipes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MealSlotRecipes_RecipeId",
                table: "MealSlotRecipes",
                column: "RecipeId");
        }
    }
}
