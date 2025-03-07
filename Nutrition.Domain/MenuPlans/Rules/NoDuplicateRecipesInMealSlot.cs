using Nutrition.Domain.Recipes.ValueObjects;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Rules;

internal class NoDuplicateRecipesInMealSlot(IReadOnlyList<RecipeId> recipeIds, RecipeId recipeId) : IBusinessRule
{
    public string Message => "Ein Rezept darf nur einmal pro Mahlzeit vorkommen.";
    public bool IsBroken() => recipeIds.Any(id => id == recipeId);
}
