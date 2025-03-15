using SharedKernel.Domain;

namespace Nutrition.Domain.Recipes.Events;

public class RecipeUpdatedDomainEvent(Guid recipeId, string name) : IDomainEvent
{
    public Guid RecipeId { get; } = recipeId;
    public string Name { get; } = name;
}