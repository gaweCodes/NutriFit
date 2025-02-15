using SharedKernel.Application;

namespace Nutrition.Application.Recipes.Queries.GetRecipe;

public class GetRecipeQuery(Guid id) : IQuery<RecipeDto?>
{
    public Guid Id { get; } = id;
}