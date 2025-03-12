namespace Nutrition.Application.Recipes.Queries.GetRecipeOverview;

public class RecipeOverviewDto(Guid id, string name)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
}