using Nutrition.Domain.MenuPlans.Rules;
using Nutrition.Domain.MenuPlans.ValueObjects;
using Nutrition.Domain.Recipes.ValueObjects;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Entities;

public class MealSlot : Entity<MealSlotId>
{
    public MealType MealType { get; private set; }
    public ICollection<RecipeId> RecipeIds { get; } = [];

    private MealSlot() { }

    internal MealSlot(MealType mealType)
    {
        Id = new MealSlotId(Guid.NewGuid());
        MealType = mealType;
    }

    internal void AddRecipe(RecipeId recipeId)
    {
        CheckRule(new NoDuplicateRecipesInMealSlot([.. RecipeIds], recipeId));
        RecipeIds.Add(recipeId);
    }
}
