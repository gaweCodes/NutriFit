using Microsoft.EntityFrameworkCore;
using Nutrition.Application.Recipes.Queries;
using Nutrition.Application.Recipes.Queries.Models;
using Nutrition.Infrastructure.Read.Database;

namespace Nutrition.Infrastructure.Read.Repositories;

internal class ReadRecipeRepository(NutritionReadDbContext dbContext) : IReadRecipeRepository
{
    public async Task<RecipeDetail?> GetRecipeDetailAsync(Guid id, CancellationToken cancellationToken) =>
        await dbContext.FindAsync<RecipeDetail>(id, cancellationToken);
    
    public async Task<List<RecipeOverview>> GetRecipesOverviewAsync(CancellationToken cancellationToken) =>
        await dbContext.Set<RecipeOverview>().ToListAsync(cancellationToken);
}