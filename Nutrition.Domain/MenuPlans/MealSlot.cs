using Nutrition.Domain.MenuPlans.Rules;
using Nutrition.Domain.Recipes;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans;

public class MealSlot : Entity
{
    public MealSlotId Id { get; } = null!;
    public MealType MealType { get; private set; }
    public ICollection<Recipe> Recipes { get; } = [];
    
    private MealSlot() { }

    internal MealSlot(MealType mealType)
    {
        Id = new MealSlotId(Guid.NewGuid());
        MealType = mealType;
    }

    internal void AddRecipe(Recipe recipe)
    {
        CheckRule(new NoDuplicateRecipesInMealSlot([.. Recipes], recipe));
        Recipes.Add(recipe);
    }
}
