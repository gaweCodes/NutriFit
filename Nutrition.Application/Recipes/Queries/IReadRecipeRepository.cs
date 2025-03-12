using Nutrition.Application.Recipes.Queries.Models;

namespace Nutrition.Application.Recipes.Queries;

public interface IReadRecipeRepository
{
    public Task<RecipeDetail> GetRecipeDetailAsync(Guid id, CancellationToken cancellationToken);
    public Task<List<RecipeOverview>> GetRecipeOverviewAsync(CancellationToken cancellationToken);
}
