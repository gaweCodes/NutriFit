﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NutriFit.MigrationService.MigrationsNutritionWrite
{
    /// <inheritdoc />
    public partial class AddedSoftDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Recipes",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Recipes");
        }
    }
}
