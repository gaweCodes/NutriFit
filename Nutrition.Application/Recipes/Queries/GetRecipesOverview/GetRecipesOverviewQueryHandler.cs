using SharedKernel.Application;

namespace Nutrition.Application.Recipes.Queries.GetRecipesOverview;

internal class GetRecipesOverviewQueryHandler(IReadRecipeRepository readRecipeRepository) : IQueryHandler<GetRecipesOverviewQuery, List<RecipeOverviewDto>>
{
    public async Task<List<RecipeOverviewDto>> Handle(GetRecipesOverviewQuery query, CancellationToken cancellationToken)
    {
        var recipesOverview = await readRecipeRepository.GetRecipesOverviewAsync(cancellationToken);

        return recipesOverview.Select(x => new RecipeOverviewDto(x.Id, x.Name)).ToList();
    }
}