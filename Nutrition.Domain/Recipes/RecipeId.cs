using SharedKernel.Domain;

namespace Nutrition.Domain.Recipes;

public class RecipeId(Guid value) : TypedIdValueBase(value);