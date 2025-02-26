using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans;

public class DayPlan : Entity
{
    internal DayPlanId Id { get; private set; } = null!;
    internal DateOnly Date { get; private set; }
    private readonly List<MealSlot> _mealSlots = [];
    
    private DayPlan() { }

    internal DayPlan(DateOnly date)
    {
        Id = new DayPlanId(Guid.NewGuid());
        Date = date;

        _mealSlots.Add(new(MealType.Breakfast));
        _mealSlots.Add(new(MealType.Lunch));
        _mealSlots.Add(new(MealType.Dinner));
    }
}