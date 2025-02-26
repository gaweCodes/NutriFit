using Microsoft.EntityFrameworkCore;
using Nutrition.Domain.MenuPlans;
using Nutrition.Infrastructure.Write.DatabaseObjectConfigurations;

namespace Nutrition.Infrastructure.Write.Database;

public class NutritionWriteDbContext(DbContextOptions<NutritionWriteDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RecipeConfiguration());
        modelBuilder.ApplyConfiguration(new MenuPlanConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}