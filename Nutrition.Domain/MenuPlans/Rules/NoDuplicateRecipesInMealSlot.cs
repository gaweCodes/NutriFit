using Nutrition.Domain.Recipes;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Rules;

internal class NoDuplicateRecipesInMealSlot(MealSlot mealSlot, Recipe recipe) : IBusinessRule
{
    public string Message => "Ein Rezept darf nur einmal pro Mahlzeit vorkommen.";

    public bool IsBroken() => mealSlot.Recipes.Any(x => x.Id == recipe.Id);
}
