using SharedKernel.Domain;

namespace Nutrition.Domain.Recipes;

public class Recipe : Entity, IAggregateRoot
{
    public RecipeId Id { get; }
    private string _name;

    private Recipe() 
    { 
        Id = new RecipeId(Guid.NewGuid());
        _name = string.Empty;
    }
    private Recipe(string name)
    {
        Id = new RecipeId(Guid.NewGuid());
        _name = name;
    }
    internal static Recipe CreateNew(string name)
    {
        return new(name);
    }
}
