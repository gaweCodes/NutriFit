    using Microsoft.EntityFrameworkCore;
using Nutrition.Infrastructure.DatabaseObjectConfigurations;

    namespace Nutrition.Infrastructure.Databases;

    public class NutritionWriteDbContext(DbContextOptions<NutritionWriteDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new RecipeConfiguration());

        base.OnModelCreating(modelBuilder);
    }
}