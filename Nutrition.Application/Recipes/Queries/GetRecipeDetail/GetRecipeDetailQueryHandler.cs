using SharedKernel.Application;

namespace Nutrition.Application.Recipes.Queries.GetRecipeDetail;

internal class GetRecipeDetailQueryHandler(IReadRecipeRepository readRecipeRepository) : IQueryHandler<GetRecipeDetailQuery, RecipeDetailDto?>
{
    public async Task<RecipeDetailDto?> Handle(GetRecipeDetailQuery query, CancellationToken cancellationToken)
    {
        var recipeDetail = await readRecipeRepository.GetRecipeDetailAsync(query.Id, cancellationToken);
        
        return recipeDetail is not null ? new RecipeDetailDto(recipeDetail.Id, recipeDetail.Name) : null;
    }
}