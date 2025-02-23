using Nutrition.Domain.Recipes.Events;
using SharedKernel.Domain;

namespace Nutrition.Domain.Recipes;

public class Recipe : Entity, IAggregateRoot
{
    public RecipeId Id { get; } = new RecipeId(Guid.NewGuid());
    public string Name { get; private set; } = string.Empty;
    public bool IsDeleted { get; private set; }

    private Recipe() { }
    private Recipe(string name)
    {
        Name = name;
        AddDomainEvent(new RecipeCreatedDomainEvent(Id.Value, Name));
    }
    public static Recipe CreateNew(string name) => new(name);
    public void UpdateRecipe(string name)
    {
        Name = name;
        AddDomainEvent(new RecipeUpdatedDomainEvent(Id.Value, Name));
    }

    public void Delete()
    {
        IsDeleted = true;
        AddDomainEvent(new RecipeDeletedDomainEvent(Id.Value));
    }
}
