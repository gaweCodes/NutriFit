using SharedKernel.Domain;

namespace Nutrition.Domain.Recipes.ValueObjects;

public class RecipeId(Guid value) : TypedIdValueBase(value);