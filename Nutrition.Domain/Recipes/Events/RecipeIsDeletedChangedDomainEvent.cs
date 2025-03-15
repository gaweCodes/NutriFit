using SharedKernel.Domain;

namespace Nutrition.Domain.Recipes.Events;

public class RecipeIsDeletedChangedDomainEvent(Guid recipeId, bool isDeleted) : IDomainEvent
{
    public Guid RecipeId { get; } = recipeId;
    public bool IsDeleted { get; } = isDeleted;
}