﻿using Microsoft.EntityFrameworkCore;
using Nutrition.Infrastructure.Read.DatabaseObjectConfigurations;

namespace Nutrition.Infrastructure.Read.Database;

public class NutritionReadDbContext(DbContextOptions<NutritionReadDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RecipeDetailConfiguration());
        modelBuilder.ApplyConfiguration(new RecipeOverviewConfiguration());
        modelBuilder.ApplyConfiguration(new MenuPlanDetailConfiguration());
        modelBuilder.ApplyConfiguration(new MenuPlanOverviewConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}