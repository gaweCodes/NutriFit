namespace Nutrition.Infrastructure.Read.DatabaseObjects;

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

    internal static RecipeDetail CreateNew(Guid id, string name) => new(id, name);
    
}
