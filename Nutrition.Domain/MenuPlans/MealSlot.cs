using SharedKernel.Domain;

namespace Nutrition.Domain.MenuPlans;

public class MealSlot : Entity
{
    internal MealSlotId Id { get; } = null!;
    internal MealType MealType { get; private set; }
    private MealSlot() { }

    internal MealSlot(MealType mealType)
    {
        Id = new MealSlotId(Guid.NewGuid());
        MealType = mealType;
    }
}
