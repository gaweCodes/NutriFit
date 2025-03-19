using SharedKernel.Domain;

namespace Nutrition.Domain.Recipes.ValueObjects;

public readonly record struct RecipeId(Guid Value) : IEntityKey;
