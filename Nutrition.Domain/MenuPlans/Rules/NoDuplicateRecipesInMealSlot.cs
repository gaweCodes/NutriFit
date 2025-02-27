using Nutrition.Domain.Recipes;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Rules;

internal class NoDuplicateRecipesInMealSlot(IReadOnlyList<Recipe> recipes, Recipe recipe) : IBusinessRule
{
    public string Message => "Ein Rezept darf nur einmal pro Mahlzeit vorkommen.";
    public bool IsBroken() => recipes.Any(x => x.Id == recipe.Id);
}
