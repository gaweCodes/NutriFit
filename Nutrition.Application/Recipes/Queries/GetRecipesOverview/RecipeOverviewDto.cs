namespace Nutrition.Application.Recipes.Queries.GetRecipesOverview;

public class RecipeOverviewDto(Guid id, string name)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
}