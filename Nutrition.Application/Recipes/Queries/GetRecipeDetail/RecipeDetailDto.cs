namespace Nutrition.Application.Recipes.Queries.GetRecipeDetail;

public class RecipeDetailDto(Guid id, string name)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
}