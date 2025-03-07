using Nutrition.Domain.Recipes;
using Nutrition.Domain.Recipes.Entities;
using Nutrition.Domain.Recipes.ValueObjects;
using Nutrition.Infrastructure.Write.Database;
using SharedKernel.Domain;

namespace Nutrition.Infrastructure.Write.Repositories;

public class RecipeRepository(NutritionWriteDbContext dbContext) : IRecipeRepository
{
    public async Task AddAsync(Recipe recipe, CancellationToken cancellationToken) => await dbContext.AddAsync(recipe, cancellationToken);

    public async Task<Recipe> GetByIdAsync(RecipeId id, CancellationToken cancellationToken)
    {
        var recipe = await dbContext.FindAsync<Recipe>(id, cancellationToken);
        return recipe is null ? throw new EntityNotFoundException(nameof(Recipe), id.Value) : recipe;
    }
    public async Task SaveChangesAsync(CancellationToken cancellationToken) => await dbContext.SaveChangesAsync(cancellationToken);
}
