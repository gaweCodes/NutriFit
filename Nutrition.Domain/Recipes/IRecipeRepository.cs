namespace Nutrition.Domain.Recipes;

public interface IRecipeRepository
{
    Task AddAsync(Recipe recipe, CancellationToken cancellationToken);
    Task<Recipe> GetByIdAsync(RecipeId id, CancellationToken cancellationToken);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}