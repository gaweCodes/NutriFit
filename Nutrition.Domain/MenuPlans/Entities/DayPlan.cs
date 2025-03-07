using Nutrition.Domain.MenuPlans.ValueObjects;
using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans.Entities;

public class DayPlan : Entity
{
    public DayPlanId Id { get; } = null!;
    public DateOnly Date { get; private set; }
    public ICollection<MealSlot> MealSlots { get; } = [];
    
    private DayPlan() { }

    internal DayPlan(DateOnly date)
    {
        Id = new DayPlanId(Guid.NewGuid());
        Date = date;

        MealSlots.Add(new(MealType.Breakfast));
        MealSlots.Add(new(MealType.Lunch));
        MealSlots.Add(new(MealType.Dinner));
    }
}