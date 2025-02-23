using Nutrition.Domain.Recipes;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans;

internal class DayPlan : Entity
{
    internal DayPlanId Id { get; } = new DayPlanId(Guid.NewGuid());
    internal DateOnly Date { get; private set; }
    private readonly List<MealSlot> _mealSlots = [];

    private DayPlan() { }
    internal DayPlan(DateOnly date, bool hasSnacking)
    {
        Date = date; 
               
        _mealSlots.Add(new MealSlot(MealType.Breakfast));
        _mealSlots.Add(new MealSlot(MealType.Lunch));
        _mealSlots.Add(new MealSlot(MealType.Dinner));
        if (hasSnacking) _mealSlots.Add(new MealSlot(MealType.Snack));
    }

    internal void AddRecipe(MealType mealType, Recipe recipe)
    {
        var mealSlot = _mealSlots.Single(x => x.MealType == mealType);
        mealSlot.AddRecipe(recipe);
    }
}
