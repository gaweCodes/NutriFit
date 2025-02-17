using SharedKernel.Domain;

namespace Nutrition.Domain.Recipes.Events;

public class RecipeDeletedDomainEvent(Guid id) : DomainEventBase
{
    public Guid RecipeId { get; } = id;
}