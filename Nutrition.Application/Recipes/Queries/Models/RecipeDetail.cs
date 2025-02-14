namespace Nutrition.Application.Recipes.Queries.Models;

public class RecipeDetail
{
    public Guid Id { get; }
    public string Name { get; }

    private RecipeDetail()
    {
        Id = Guid.Empty;
        Name = string.Empty;
    }
    private RecipeDetail(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public static RecipeDetail CreateNew(Guid id, string name) => new(id, name);
}
