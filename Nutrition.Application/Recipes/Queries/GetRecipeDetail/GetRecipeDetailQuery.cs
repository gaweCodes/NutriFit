using SharedKernel.Application;

namespace Nutrition.Application.Recipes.Queries.GetRecipeDetail;

public class GetRecipeDetailQuery(Guid id) : IQuery<RecipeDetailDto>
{
    public Guid Id { get; } = id;
}