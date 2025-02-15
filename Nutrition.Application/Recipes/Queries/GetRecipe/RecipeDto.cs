namespace Nutrition.Application.Recipes.Queries.GetRecipe;

public class RecipeDto(string name)
{
    public string Name { get; } = name;
}