using Nutrition.Domain.Recipes.Events;
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
        AddDomainEvent(new RecipeCreatedDomainEvent(Id.Value, _name));
    }
    public static Recipe CreateNew(string name)
    {
        return new(name);
    }
}
