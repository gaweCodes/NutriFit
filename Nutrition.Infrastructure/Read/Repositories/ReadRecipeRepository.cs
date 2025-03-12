using Microsoft.EntityFrameworkCore;
using Nutrition.Application.Recipes.Queries;
using Nutrition.Application.Recipes.Queries.Models;
using Nutrition.Infrastructure.Read.Database;
using SharedKernel.Domain;

namespace Nutrition.Infrastructure.Read.Repositories;

internal class ReadRecipeRepository(NutritionReadDbContext dbContext) : IReadRecipeRepository
{
    public async Task<RecipeDetail> GetRecipeDetailAsync(Guid id, CancellationToken cancellationToken)
    {
        var recipeDetail = await dbContext.FindAsync<RecipeDetail>(id, cancellationToken);
        return recipeDetail is null ? throw new EntityNotFoundException(nameof(RecipeDetail), id) : recipeDetail;
    }

    public async Task<List<RecipeOverview>> GetRecipeOverviewAsync(CancellationToken cancellationToken) =>
        await dbContext.Set<RecipeOverview>().ToListAsync(cancellationToken);
}