using SharedKernel.Domain;

namespace Nutrition.Domain.Recipes.Events;

public class RecipeCreatedDomainEvent(Guid id, string name) : DomainEventBase
{
    public Guid RecipeId { get; } = id;
    public string Name { get; } = name;
}