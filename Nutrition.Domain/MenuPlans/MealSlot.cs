using Nutrition.Domain.Recipes;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans;

internal class MealSlot : Entity
{
    internal MealSlotId MealSlotId { get; }
    private MealType _mealType;
    private readonly List<Recipe> _recipes = [];
    private MealSlot() { }

    internal MealSlot(MealType mealType)
    {
        MealSlotId = new MealSlotId(Guid.NewGuid());
        _mealType = mealType;
    }

}
