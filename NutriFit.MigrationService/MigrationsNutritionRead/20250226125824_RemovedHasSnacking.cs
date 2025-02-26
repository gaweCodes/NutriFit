using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriFit.MigrationService.MigrationsNutritionRead
{
    /// <inheritdoc />
    public partial class RemovedHasSnacking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasSnacking",
                table: "MenuPlanOverviews");

            migrationBuilder.DropColumn(
                name: "HasSnacking",
                table: "MenuPlanDetails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasSnacking",
                table: "MenuPlanOverviews",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "HasSnacking",
                table: "MenuPlanDetails",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
