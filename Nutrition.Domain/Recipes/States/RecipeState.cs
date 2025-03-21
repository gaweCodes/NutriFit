using Nutrition.Domain.Recipes.Events;
using Nutrition.Domain.Recipes.ValueObjects;
using SharedKernel.Domain;

namespace Nutrition.Domain.Recipes.States;

public class RecipeState : AggregateState<RecipeId>
{
    internal RecipeState() { }
    internal string Name { get; private set; } = string.Empty;
    internal bool IsDeleted { get; private set; }

    protected void Apply(RecipeCreatedDomainEvent recipeCreatedEvent)
    {
        Id = new RecipeId(recipeCreatedEvent.RecipeId);
        Name = recipeCreatedEvent.Name;
    }

    protected void Apply(RecipeUpdatedDomainEvent recipeUpdatedDomainEvent)
    {
        Id = new RecipeId(recipeUpdatedDomainEvent.RecipeId);
        Name = recipeUpdatedDomainEvent.Name;
    }

    protected void Apply(RecipeIsDeletedChangedDomainEvent recipeIsDeletedChangedDomainEvent)
    {
        Id = new RecipeId(recipeIsDeletedChangedDomainEvent.RecipeId);
        IsDeleted = recipeIsDeletedChangedDomainEvent.IsDeleted;
    }
}
