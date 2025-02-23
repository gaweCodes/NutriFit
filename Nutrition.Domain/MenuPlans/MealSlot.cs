using Nutrition.Domain.MenuPlans.Rules;
using Nutrition.Domain.Recipes;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans;

internal class MealSlot : Entity
{
    internal MealSlotId Id { get; } = new MealSlotId(Guid.NewGuid());
    internal MealType MealType { get; private set; }
    private readonly List<Recipe> _recipes = [];
    internal IReadOnlyCollection<Recipe> Recipes => _recipes.AsReadOnly();

    private MealSlot() { }

    internal MealSlot(MealType mealType) => MealType = mealType;
    
    internal void AddRecipe(Recipe recipe)
    {
        CheckRule(new NoDuplicateRecipesInMealSlot(this, recipe));
        _recipes.Add(recipe);
    }
}
