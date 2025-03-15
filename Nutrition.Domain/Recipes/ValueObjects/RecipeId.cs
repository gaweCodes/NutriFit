using SharedKernel.Domain;

namespace Nutrition.Domain.Recipes.ValueObjects;

public record struct RecipeId(Guid Value) : IAggregateId;
