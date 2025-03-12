using SharedKernel.Application;

namespace Nutrition.Application.Recipes.Queries.GetRecipeOverview;

internal class GetRecipeOverviewQueryHandler(IReadRecipeRepository readRecipeRepository) : IQueryHandler<GetRecipeOverviewQuery, List<RecipeOverviewDto>>
{
    public async Task<List<RecipeOverviewDto>> Handle(GetRecipeOverviewQuery query, CancellationToken cancellationToken)
    {
        var recipeOverview = await readRecipeRepository.GetRecipeOverviewAsync(cancellationToken);
        return [.. recipeOverview.Select(x => new RecipeOverviewDto(x.Id, x.Name))];
    }
}