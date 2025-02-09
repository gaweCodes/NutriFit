using Microsoft.EntityFrameworkCore;
using Nutrition.Infrastructure.Write.DatabaseObjectConfigurations;

namespace Nutrition.Infrastructure.Write.Database;

public class NutritionWriteDbContext(DbContextOptions<NutritionWriteDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RecipeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}