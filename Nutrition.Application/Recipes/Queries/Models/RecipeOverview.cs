namespace Nutrition.Application.Recipes.Queries.Models;

public class RecipeOverview
{
    public Guid Id { get; }
    public string Name { get; }

    private RecipeOverview()
    {
        Id = Guid.Empty;
        Name = string.Empty;
    }
    private RecipeOverview(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static RecipeOverview CreateNew(Guid id, string name) => new(id, name);
}
