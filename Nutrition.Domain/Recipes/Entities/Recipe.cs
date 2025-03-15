using Nutrition.Domain.Recipes.Events;
using Nutrition.Domain.Recipes.ValueObjects;
using SharedKernel.Domain;

namespace Nutrition.Domain.Recipes.Entities;

public class Recipe : Entity<RecipeId>, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;
    public bool IsDeleted { get; private set; }

    private Recipe() { }
    private Recipe(string name)
    {
        var domainEvent = new RecipeCreatedDomainEvent(Guid.NewGuid(), name);
        Apply(domainEvent);
        AddUncommittedEvent(domainEvent);
    }
    public static Recipe CreateNew(string name) => new(name);
    public void UpdateRecipe(string name)
    {
        var domainEvent = new RecipeUpdatedDomainEvent(Id.Value, name);
        Apply(domainEvent);
        AddUncommittedEvent(domainEvent);
    }

    public void Delete()
    {
        var domainEvent = new RecipeIsDeletedChangedDomainEvent(Id.Value, true);
        Apply(domainEvent);
        AddUncommittedEvent(domainEvent);
    }

    private void Apply(RecipeCreatedDomainEvent recipeCreatedEvent)
    {
        Id = new RecipeId(recipeCreatedEvent.RecipeId);
        Name = recipeCreatedEvent.Name;
        Version++;
    }

    private void Apply(RecipeUpdatedDomainEvent recipeUpdatedDomainEvent)
    {
        Id = new RecipeId(recipeUpdatedDomainEvent.RecipeId);
        Name = recipeUpdatedDomainEvent.Name;
        Version++;
    }

    private void Apply(RecipeIsDeletedChangedDomainEvent recipeIsDeletedChangedDomainEvent)
    {
        Id = new RecipeId(recipeIsDeletedChangedDomainEvent.RecipeId);
        IsDeleted = recipeIsDeletedChangedDomainEvent.IsDeleted;
        Version++;
    }
}
