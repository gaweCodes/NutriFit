using Nutrition.Domain.Recipes;
using Nutrition.Infrastructure.Write.Database;

namespace Nutrition.Infrastructure.Write.Repositories;

public class RecipeRepository(NutritionWriteDbContext dbContext) : IRecipeRepository
{
    public async Task AddAsync(Recipe recipe, CancellationToken cancellationToken) => await dbContext.AddAsync(recipe, cancellationToken);

    public async Task<Recipe> GetByIdAsync(RecipeId id, CancellationToken cancellationToken)
    {
        var recipe = await dbContext.FindAsync<Recipe>(id, cancellationToken);
        return recipe is null ? throw new ArgumentException($"There is no {nameof(Recipe)} with id = {id}") : recipe;
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken) => await dbContext.SaveChangesAsync(cancellationToken);
}
