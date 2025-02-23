using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans;

internal class DayPlan : Entity
{
    internal DayPlanId Id { get; }
    private readonly DateOnly _date;
    private readonly List<MealSlot> _mealSlots = [];

    private DayPlan() { }
    internal DayPlan(DateOnly date, bool hasSnacking)
    {
        Id = new DayPlanId(Guid.NewGuid());
        _date = date; 
               
        _mealSlots.Add(new MealSlot(MealType.Breakfast));
        _mealSlots.Add(new MealSlot(MealType.Lunch));
        _mealSlots.Add(new MealSlot(MealType.Dinner));
        if (hasSnacking) _mealSlots.Add(new MealSlot(MealType.Snack));
    }
}
