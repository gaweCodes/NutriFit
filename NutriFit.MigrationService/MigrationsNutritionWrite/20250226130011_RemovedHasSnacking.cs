using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriFit.MigrationService.MigrationsNutritionWrite
{
    /// <inheritdoc />
    public partial class RemovedHasSnacking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasSnacking",
                table: "MenuPlans");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasSnacking",
                table: "MenuPlans",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }
    }
}
