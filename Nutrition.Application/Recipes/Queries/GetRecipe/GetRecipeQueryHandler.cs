using SharedKernel.Application;

namespace Nutrition.Application.Recipes.Queries.GetRecipe;

internal class GetRecipeQueryHandler(IReadRecipeRepository readRecipeRepository) : IQueryHandler<GetRecipeQuery, RecipeDto>
{
    public async Task<RecipeDto> Handle(GetRecipeQuery query, CancellationToken cancellationToken)
    {
        var recipe = await readRecipeRepository.GetRecipeDetailAsync(query.Id, cancellationToken);
        return new RecipeDto(recipe.Name);
    }
}